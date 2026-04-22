using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Intro,
    Playing,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State = GameState.Intro;

    public int Lives = 3;
    private bool isGameOver = false;

    public float playStartTime;
    public float playTime;

    public int HighScore;
    public int MyScore;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (State == GameState.Intro)
        {
            isGameOver = false;
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                State = GameState.Playing;
                UIManager.Instance.IntroUI.SetActive(false);
                UIManager.Instance.ItemSpawner.SetActive(true);

                playStartTime = Time.time;
            }
        }
        else if (State == GameState.Playing)
        {
            if (Lives == 0)
            {
                State = GameState.GameOver;
                UIManager.Instance.ItemSpawner.SetActive(false);
            }
        }
        else if (State == GameState.GameOver)
        {
            if (isGameOver == false)
            {
                Invoke("GameOverEvent", 3f);
            }
            isGameOver = true;
        }
    }

    public int CalculateScore()
    {
        int score = Mathf.FloorToInt(Time.time - playStartTime);
        return score;
    }

    private void GameOverEvent()
    {
        Lives = 3;
        State = GameState.Intro;
        SceneManager.LoadScene("Main");
        Debug.Log("Scene Reload!");
    }


    //Live를 1을 더해 3을 넘지 않게 한다.
    public void AddLive()
    {
        Lives = Mathf.Min(Lives + 1, 3);
    }

    public void RemoveLive()
    {
        Lives--;
    }

    private void GameOver()
    {
        State = GameState.GameOver;
        UIManager.Instance.ItemSpawner.SetActive(false);
    }
}
