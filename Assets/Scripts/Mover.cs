using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // 배경 스크립트를 못 찾을 경우의 기본 속도
    [SerializeField] private float speedMultiplier = 1f; // 배경 속도에 곱해질 배율 (기본값: 1, 속도 같음)

    private BackgroundScroll _bgScroll;

    private void Start()
    {
        // 씬(Scene)에 존재하는 BackgroundScroll 스크립트를 자동으로 찾아옵니다.
        _bgScroll = FindFirstObjectByType<BackgroundScroll>();
    }

    private void Update()
    {
        // 배경을 성공적으로 찾았다면 배경의 속도(scrollSoeed)를 가져오고, 그렇지 않으면 기본 속도를 사용합니다.
        float baseSpeed = (_bgScroll != null) ? _bgScroll.scrollSoeed : moveSpeed;

        // speedMultiplier를 곱해서 인스펙터(Inspector)에서 속도를 조절할 수 있도록 합니다.
        float currentSpeed = baseSpeed * speedMultiplier;

        transform.position += Vector3.left * currentSpeed * Time.deltaTime;
    }
}

