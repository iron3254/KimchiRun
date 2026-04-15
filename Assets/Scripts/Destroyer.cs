using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private Camera mainCamera;
    private SpriteRenderer spriteRenderer;

    private Transform f;

    void Awake()
    {
        mainCamera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 카메라와 오브젝트가 초기화되지 않았을 경우를 대비한 예외 처리
        if (mainCamera == null || spriteRenderer == null) return;

        float cameraLeftEdge = mainCamera.transform.position.x - mainCamera.orthographicSize * mainCamera.aspect;
        float objectRightEdge = spriteRenderer.bounds.max.x;

        if (objectRightEdge < cameraLeftEdge)
        {
            Destroy(gameObject);
        }
    }
}

