using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Start()
    {
        string[] enemies = { "좀비", "스켈레톤", "크리퍼", "거미", "엔더맨" };
        for (int i = 0; i < enemies.Length; i++)
        {
            Debug.Log(enemies[i]);
        }
    }
}

