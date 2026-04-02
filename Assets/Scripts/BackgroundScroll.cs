using System.Net.NetworkInformation;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float scrollSoeed = 3.5f;
    public Material mat;
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;

    }

    void Update()
    {
        // Tiling 뿐만 아니라, 오브젝트의 화면상 크기(Scale)까지 고려하여 보정합니다.
        // 이렇게 하면 scrollSoeed가 "실제 게임 월드에서 움직이는 거리(속도)"로 완전히 통일됩니다.
        float realSpeed = scrollSoeed * mat.mainTextureScale.x / transform.lossyScale.x;
        float offset = realSpeed * Time.deltaTime;

        mat.mainTextureOffset += new Vector2(offset, 0);
    }
}
