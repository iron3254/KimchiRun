using UnityEngine;
using UnityEngine.InputSystem;

public class NumberGame : MonoBehaviour
{
    void Start()
    {
        Debug.Log("=================");
        Debug.Log("숫자 맞추기 게임 시작");
        Debug.Log("1~5 사이의 숫자를 입력하세요.");
        Debug.Log("[1] [2] [3] [4] [5]");
    }

    void Update()
    {
        int input = -1;
        if (Input.GetKeyDown(KeyCode.Alpha1)) input = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2)) input = 2;
        if (Input.GetKeyDown(KeyCode.Alpha3)) input = 3;
        if (Input.GetKeyDown(KeyCode.Alpha4)) input = 4;
        if (Input.GetKeyDown(KeyCode.Alpha5)) input = 5;

        if (input != -1)
        {
            Debug.Log("input : " + input);
            CheckAnswer(input);
        }
    }
    int tryCount = 0;

    private void CheckAnswer(int input)
    {
        tryCount++;
        Debug.Log($"입력: {input}, 시도 횟수 : {tryCount}");
    }
}

