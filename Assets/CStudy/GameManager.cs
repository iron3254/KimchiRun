using UnityEngine;
using UnityEngine.InputSystem;

public enum GameState
{
    Playing,
    Paused,
    GameOver,
    Clear
}
public class GameManager : MonoBehaviour
{
    public EnemyController enemyController;
    private GameState currentState = GameState.Playing;

    void Update()
    {
        Keyboard curkey = Keyboard.current;
        if (curkey != null && curkey.uKey.wasPressedThisFrame)
        {
            currentState = GameState.Playing;
        }

        if (curkey != null && curkey.iKey.wasPressedThisFrame)
        {
            currentState = GameState.Paused;
        }

        if (curkey != null && curkey.oKey.wasPressedThisFrame)
        {
            currentState = GameState.GameOver;
        }

        if (curkey != null && curkey.pKey.wasPressedThisFrame)
        {
            currentState = GameState.Clear;
        }


        switch (currentState)
        {
            case GameState.Playing:
                Debug.Log("게임 진행 중");
                break;
            case GameState.Paused:
                Debug.Log("일시 정지");
                break;
            case GameState.GameOver:
                Debug.Log("게임 오버!");
                break;
            case GameState.Clear:
                Debug.Log("클리어!");
                break;
        }
    }
}

