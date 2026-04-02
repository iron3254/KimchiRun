using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce = 7f;
    public int maxJumpCount = 2; // 최대 허용 점프 횟수 (2 = 더블점프)

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
        // 스페이스 키를 누르고 있고, 남은 점프 횟수가 있을 때 점프
        if (Input.GetKeyDown(KeyCode.Space) && currentJumpCount < maxJumpCount)
        {
            // 2D 환경이므로 Vector2를 사용합니다.
            rigid.linearVelocity = new Vector2(rigid.linearVelocity.x, jumpForce);
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
    }

    // 바닥과 충돌했을 때 다시 점프할 수 있도록 횟수를 초기화합니다.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentJumpCount = 0;

        // 바닥에 닿았으므로 점프 애니메이션 파라미터 끄기 (달리기/대기 애니메이션으로 복귀)
        if (anim != null)
        {
            anim.SetBool("isJumping", false);
        }
    }
}

