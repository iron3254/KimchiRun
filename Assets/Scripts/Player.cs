using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("점프 설정")]
    [Tooltip("원하는 최대 점프 높이 (단위: 미터/유닛)")]
    [Range(1f, 10f)]
    public float jumpHeight = 3f; // 고정할 점프 높이
    [Tooltip("최고점에 도달하는 데 걸리는 시간 (작을수록 올라가는 속도가 빠름)")]
    [Range(0.1f, 1.5f)]
    public float timeToJumpApex = 0.4f; // 올라가는 시간
    [Range(0.1f, 10f)]
    public float fallGravityScale = 1f; // 내려올 때 (낙하 중) 중력 스케일
    public int maxJumpCount = 1; // 최대 허용 점프 횟수 (1 = 단일 점프)

    private Rigidbody2D rigid;
    private Animator anim;
    private int currentJumpCount = 0;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 현재 설정된 높이와 시간에 맞춰 필요한 중력과 초기 속도를 매 프레임 계산
        // 초기속도 v = 2 * h / t, 중력 g = 2 * h / t^2
        float requiredGravity = (2f * jumpHeight) / (timeToJumpApex * timeToJumpApex);
        float calculatedJumpGravityScale = requiredGravity / Mathf.Abs(Physics2D.gravity.y);

        // 스페이스 키를 꾹 누르고 있어도 연속해서 점프가 되도록 GetKey 사용
        if (Input.GetKey(KeyCode.Space) && currentJumpCount < maxJumpCount)
        {
            // 설정한 jumpHeight와 timeToJumpApex에 맞춰 점프 속도 자동 계산
            float calculatedSpeed = (2f * jumpHeight) / timeToJumpApex;

            // 2D 환경이므로 Vector2를 사용합니다.
            rigid.linearVelocity = new Vector2(rigid.linearVelocity.x, calculatedSpeed);
            currentJumpCount++; // 점프할 때마다 횟수 증가

            // 점프 애니메이션 파라미터 켜기
            if (anim != null)
            {
                // 공중에서 더블점프를 뛸 때도 다시 모션을 처음부터 재생하려면 SetTrigger 등을 쓰는 것이 더 좋지만
                // 현재 설정해둔 isJumping Bool 파라미터를 그대로 유지합니다.
                anim.SetBool("isJumping", true);
            }
        }

        // 애니메이터가 연결되어 있고 공중에 떠 있을 때(선택적) 높이와 오르내림 속도를 애니메이터에 전달합니다.
        if (anim != null)
        {
            // 1. 위로 올라가는지 / 아래로 떨어지는지 (+/- 속도)
            anim.SetFloat("VelocityY", rigid.linearVelocity.y);

            // 2. 현재 절대적인 높이 위치
            anim.SetFloat("HeightY", transform.position.y);
        }

        // --- 점프/낙하 시간에 따른 중력(속도) 조절 ---
        if (rigid.linearVelocity.y > 0.01f)
        {
            // 위로 올라가는 중 (계산된 정확한 중력 적용)
            rigid.gravityScale = calculatedJumpGravityScale;
        }
        else if (rigid.linearVelocity.y < -0.01f)
        {
            // 아래로 떨어지는 중
            rigid.gravityScale = fallGravityScale;
        }
        else
        {
            // 바닥에 있거나 정점(잠깐 멈춘 상태)일 때
            rigid.gravityScale = 1f;
        }
    }

    // 바닥과 충돌했을 때 다시 점프할 수 있도록 횟수를 초기화합니다.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentJumpCount = 0;

        // 바닥에 닿았으므로 점프 애니메이션 파라미터 끄기 (달리기/대기 애니메이션으로 복귀)
        if (anim != null)
        {
            anim.SetBool("isJumping", false);
            // PlayerJump 애니메이션의 진행 정도를 즉시 0(처음)으로 완전히 초기화
            anim.Play("PlayerJump", -1, 0f);
        }
    }
}

