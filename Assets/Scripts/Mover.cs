using UnityEngine;

public class Mover : MonoBehaviour
{
    // 유니티 인스펙터 창에서 각각의 오브젝트마다 속도를 다르게 설정할 수 있습니다. 
    // 1이면 배경과 완벽히 같은 속도이며, 1보다 크면 더 빠르고, 1보다 작으면 느리게 이동합니다.
    public float speedMultiplier = 1f;

    void Update()
    {
        // 기본 속도는 GameManager가 계산해주는 현재 게임(배경) 속도로 맞춥니다.
        float baseSpeed = GameManager.Instance.CalculateGameSpeed();

        // 기본 속도에 배율(Multiplier)을 곱하여 최종 이동 속도를 적용합니다.
        float moveSpeed = baseSpeed * speedMultiplier;

        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }
}