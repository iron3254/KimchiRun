using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // 배경 스크립트를 못 찾을 경우의 기본 속도

    private BackgroundScroll _bgScroll;

    private void Start()
    {
        // 씬(Scene)에 존재하는 BackgroundScroll 스크립트를 자동으로 찾아옵니다.
        _bgScroll = FindFirstObjectByType<BackgroundScroll>();
    }

    private void Update()
    {
        // 배경을 성공적으로 찾았다면 배경의 속도(scrollSoeed)를 그대로 사용해 속도를 완벽히 일치시킵니다.
        float currentSpeed = (_bgScroll != null) ? _bgScroll.scrollSoeed : moveSpeed;

        transform.position += Vector3.left * currentSpeed * Time.deltaTime;
    }
}

