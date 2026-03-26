using UnityEngine;

    public class EnemyController : MonoBehaviour
{
    public string enemyName = "Dragon";
    public int enemyHP = 1200;
    public float enemyMoveSpeed = 3.0f;
    private bool isAttack = true;
    void Start()
    {
        Debug.Log(enemyName + "이 나타났다");
        Debug.Log("체력: " + enemyHP);
        Debug.Log("속도:" + enemyMoveSpeed);
        Debug.Log("적대여부: " + isAttack);

    }

}

