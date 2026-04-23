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


    public float PlayStartTime;
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

    private void Start()
    {
        // 게임 시작 시 기기에 저장된 최고 점수를 불러옵니다.
        HighScore = GetHighScore();
    }

    private void Update()
    {
        if (State == GameState.Intro)
        {

            // 스페이스바나 마우스 클릭 시 게임 시작
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                State = GameState.Playing;
                UIManager.Instance.IntroUI.SetActive(false);
                UIManager.Instance.ItemSpawner.SetActive(true);

                PlayStartTime = Time.time;
            }
        }
        else if (State == GameState.Playing)
        {
            if (Lives == 0)
            {
                State = GameState.GameOver;
                UIManager.Instance.ItemSpawner.SetActive(false);
                SaveHighScore();
            }
        }
        else if (State == GameState.GameOver)
        {
            // 게임오버 상태일 때 스페이스바를 누르면 씬 리로드
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Lives = 3; // 라이프 초기화
                State = GameState.Intro;
                SceneManager.LoadScene("Main");
            }
        }
    }

    // 라이프를 1을 더해 3을 넘지 않게 합니다.
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
        SaveHighScore();
    }

    public int CalculateScore()
    {
        return Mathf.FloorToInt(Time.time - PlayStartTime);
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
    }

    public void SaveHighScore()
    {
        MyScore = Mathf.FloorToInt(CalculateScore());
        HighScore = PlayerPrefs.GetInt("HighScore", 0);

        if (MyScore > HighScore)
        {
            // 신기록 갱신
            PlayerPrefs.SetInt("HighScore", MyScore);
            PlayerPrefs.Save();
        }
    }

    public float CalculateGameSpeed()
    {
        if (State != GameState.Playing)
        {
            return 5f;
        }
        float speed = 8f + (0.5f * Mathf.Floor(CalculateScore() / 10f));
        return Mathf.Min(speed, 30f);
    }


}
