using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerState
    {
        Run = 0,
        Jump = 1,
        Land = 2
    }

    [Header("점프 설정")]
    [Range(1f, 10f)]
    [SerializeField] private float jumpHeight = 3f;

    [Range(0.1f, 1.5f)]
    [SerializeField] private float timeToJumpApex = 0.4f;

    [Range(0.1f, 10f)]
    [SerializeField] private float fallGravityScale = 1f;

    [SerializeField] private int maxJumpCount = 1;

    private Rigidbody2D rigid;
    private Animator anim;

    private int currentJumpCount;
    private bool isGrounded;
    private bool isJumpAnim;
    private bool isLandAnim;

    public bool isInvincible = false;

    private new Collider2D collider;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        currentJumpCount = 0;
        isGrounded = false;
    }

    private void Update()
    {
        UpdateAnimationStates();
        HandleJump();
        HandleGravityAndFall();
    }

    // 1. 애니메이터 상태 확인 및 갱신을 전담하는 메서드
    private void UpdateAnimationStates()
    {
        if (anim == null) return;

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        isJumpAnim = stateInfo.IsName("PlayerJump");
        isLandAnim = stateInfo.IsName("PlayerLand");

        // 착지 애니메이션 중이라면 달리기 상태로 변경
        if (isLandAnim)
        {
            ChangeState(PlayerState.Run);
        }
    }

    // 2. 점프 입력을 받고 물리를 적용하는 메서드
    private void HandleJump()
    {
        bool isJumpKeyPressed = Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);
        bool canJump = !isJumpAnim; // 점프 애니메이션이 한창 진행 중일 때는 중복 점프 애니메이션 진행 방지

        if (canJump && isJumpKeyPressed && currentJumpCount < maxJumpCount)
        {
            float jumpSpeed = (2f * jumpHeight) / timeToJumpApex;
            rigid.linearVelocity = new Vector2(rigid.linearVelocity.x, jumpSpeed);

            currentJumpCount++;
            isGrounded = false; // 공중에 뜸

            if (anim != null)
            {
                anim.Play("PlayerJump", -1, 0f); // 즉시 점프부터 처음부터 재생
            }
            ChangeState(PlayerState.Jump);
        }
    }

    // 3. 중력 조절 및 하강(낙하) 관련 처리를 전담하는 메서드
    private void HandleGravityAndFall()
    {
        float velocityY = rigid.linearVelocity.y;

        if (!isGrounded)
        {
            if (velocityY > 0.01f) // 상승 중
            {
                // 포물선 정점 전까지 역동적인 중력 배율 적용
                float requiredGravity = (2f * jumpHeight) / (timeToJumpApex * timeToJumpApex);
                rigid.gravityScale = requiredGravity / Mathf.Abs(Physics2D.gravity.y);
            }
            else if (velocityY < -0.01f) // 하강 중
            {
                rigid.gravityScale = fallGravityScale;

                // 점프 애니메이션 상태가 아닐 때 떨어지는 연출
                if (anim != null && !isJumpAnim && !isLandAnim)
                {
                    anim.Play("PlayerJump", -1, 0.5f);
                    ChangeState(PlayerState.Jump);
                }
            }
        }
        else // 바닥에 붙어있을 때
        {
            rigid.gravityScale = 1f;

            // 바닥에 있는데 찰나의 버그로 애니메이터가 아직 점프 상태일 때 달리기로 복구
            if (anim != null && anim.GetInteger("state") == (int)PlayerState.Jump)
            {
                ChangeState(PlayerState.Run);
            }
        }
    }

    // 상태 변경 코드의 중복을 막는 편의성 메서드
    private void ChangeState(PlayerState newState)
    {
        if (anim != null)
        {
            anim.SetInteger("state", (int)newState);
        }
    }

    private bool IsGround(GameObject obj)
    {
        // "Ground", "Platform" 태그가 있거나 Platform 스크립트를 가지고 있다면 바닥으로 인정합니다.
        return obj.CompareTag("Ground");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsGround(collision.gameObject))
        {
            isGrounded = true;
            currentJumpCount = 0;
            ChangeState(PlayerState.Land);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // 찰나의 프레임에서 바닥 인식이 풀리는 것을 방지
        if (IsGround(collision.gameObject))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (IsGround(collision.gameObject))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        string targetTag = other.tag;

        if (targetTag == "Enemy" || targetTag == "Food" || targetTag == "Gold")
        {
            Debug.Log($"Player triggerEnter in {targetTag} : {other.gameObject.name}");
            Destroy(other.gameObject);

            switch (targetTag)
            {
                case "Enemy":
                    if (!isInvincible)
                    {
                        Damage();
                    }
                    break;
                case "Food":
                    Heal();
                    break;
                case "Gold":
                    StartInvincible();
                    break;
            }
        }
    }

    private void StartInvincible()
    {
        isInvincible = true;
        Invoke("StopInvincible", 5f);
    }

    private void StopInvincible()
    {
        isInvincible = false;
    }

    private void Heal()
    {
        GameManager.Instance.AddLive();
    }

    private void Damage()
    {
        GameManager.Instance.RemoveLive();
        if (GameManager.Instance.Lives <= 0)
        {
            Debug.Log("Game Over");
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        collider.enabled = false;
        anim.enabled = false;
        rigid.AddForceY(20f, ForceMode2D.Impulse);
    }
}
