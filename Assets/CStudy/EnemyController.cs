using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyController : MonoBehaviour
{
    GameManager gameManager;
    void Start()
    {
        TakeDamage(10);

    }
    public void TakeDamage(int damage)
    {
        Debug.Log("damage " + damage);
        return;
    }
}
