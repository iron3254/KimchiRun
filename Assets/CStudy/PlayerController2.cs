using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController2 : MonoBehaviour
{
    private int hp = 100;
    private int score = 0;

    void TakeDamage(int damage)
    {
        hp = hp - damage;
        Debug.Log("데미지: " + damage + " / 남은 체력: " + hp);
    }

    void Start()
    {
        Debug.Log("takedamage 호출");
        TakeDamage(25);
        TakeDamage(40);

        int max = GetMaxHP();
        Debug.Log("최대 체력: " + max);

    }

    int GetMaxHP()
    {
        return 100;
    }

    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        if (keyboard.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("스페이스바를 눌렀습니다!");
        }

        if (keyboard.wKey.isPressed)
        {
            Debug.Log("W 키를 누르는 중...");
        }
    }
}

