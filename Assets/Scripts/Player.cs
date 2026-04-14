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

    private Rigidbody2D _rigid;
    private Animator _anim;
    private int _currentJumpCount;

    private void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _currentJumpCount = 0;
    }

    private void Update()
    {
        bool canJump = true;
        bool isJumpAnim = false;
        bool isLandAnim = false;

        // 1. 애니메이터 상태 확인 및 갱신
        if (_anim != null)
        {
            AnimatorStateInfo stateInfo = _anim.GetCurrentAnimatorStateInfo(0);
            isJumpAnim = stateInfo.IsName("PlayerJump");
            isLandAnim = stateInfo.IsName("PlayerLand");

            if (isLandAnim)
            {
                _anim.SetInteger("state", (int)PlayerState.Run);
            }
            else if (isJumpAnim)
            {
                canJump = false;
            }
        }

        // 2. 점프 처리
        if (canJump && Input.GetKey(KeyCode.Space) && _currentJumpCount < maxJumpCount)
        {
            float jumpSpeed = (2f * jumpHeight) / timeToJumpApex;
            _rigid.linearVelocity = new Vector2(_rigid.linearVelocity.x, jumpSpeed);
            _currentJumpCount++;

            if (_anim != null)
            {
                _anim.Play("PlayerJump", -1, 0f);
                _anim.SetInteger("state", (int)PlayerState.Jump);
            }
        }

        // 3. 중력 및 하강 애니메이션 처리
        float velocityY = _rigid.linearVelocity.y;

        if (velocityY > 0.01f) // 상승
        {
            float requiredGravity = (2f * jumpHeight) / (timeToJumpApex * timeToJumpApex);
            _rigid.gravityScale = requiredGravity / Mathf.Abs(Physics2D.gravity.y);
        }
        else if (velocityY < -0.01f) // 하강
        {
            _rigid.gravityScale = fallGravityScale;

            if (_anim != null && !isJumpAnim && !isLandAnim)
            {
                _anim.Play("PlayerJump", -1, 0.5f);
                _anim.SetInteger("state", (int)PlayerState.Jump);
            }
        }
        else // 바닥 처리
        {
            _rigid.gravityScale = 1f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _currentJumpCount = 0;

        if (_anim != null)
        {
            _anim.SetInteger("state", (int)PlayerState.Land);
        }
    }
}
