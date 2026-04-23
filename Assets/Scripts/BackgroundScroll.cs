using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    private Material material;

    private void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        float gameSpeed = GameManager.Instance.CalculateGameSpeed();

        // 오브젝트 넓이(Scale)와 머티리얼 타일링(Tiling) 비율을 역산하여
        // 실제 게임 오브젝트들의 이동 속도(Mover)와 배경 텍스처 이동 속도를 완벽히 일치시킵니다.

        float offsetSpeed = gameSpeed * material.mainTextureScale.x / transform.lossyScale.x;


        material.mainTextureOffset += new Vector2(offsetSpeed * Time.deltaTime, 0);
    }
}