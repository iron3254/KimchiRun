using UnityEngine;
using Unity.Burst.Intrinsics;
using UnityEngine.InputSystem;
using TMPro;
using NUnit.Framework;

public class PlayerController : MonoBehaviour
{
    private int hp = 100;
    private bool isJumping = false;

    void Start()
    {
        Debug.Log("게임 시작! [Space] 점프, [A] 공격, [S] 방어, [Z] 데미지");
    }

    void Update()
    {
        var key = Keyboard.current;
        if (key == null) return;

        if (key.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("[Space] 점프");
            if (isJumping)
            {
                Debug.Log("공중에 뜸");
            }
            else 
            {
                isJumping = true;
                Debug.Log("점프");
            }
        }
        if (key.aKey.wasPressedThisFrame)
            {
                Debug.Log("[A] 공격");
                if (isJumping)
                {
                    isJumping = false;
                    Debug.Log("낙공");
                }
                else 
                {
                    Debug.Log("일반공격");
                }
            }
        if (key.sKey.wasPressedThisFrame)
        {
            Debug.Log("[S] 방어");
            Debug.Log("체력상태: " + GetHPStatus());
        }
        if (key.zKey.wasPressedThisFrame)
        {
            Debug.Log("[Z] 데미지");
            int damage = 25;
            TakeDamage(damage);
            Debug.Log("데미지 공격: " + damage);
            Debug.Log("체력상태: " + GetHPStatus());
        }
        
        
    }
    private string GetHPStatus()
    {
        if (hp >= 70)
        {
            return "건강함";
        }
        else if (hp >= 30)
        {
            return "위험!";
        }
        else
        {
            return "빈사!";
        }
    }
    private void TakeDamage(int damage)
    {
        hp = hp - damage;
        if(hp < 0) hp = 0;
    }
    

    
     
 
}