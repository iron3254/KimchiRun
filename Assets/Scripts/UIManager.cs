using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject IntroUI;
    public GameObject ItemSpawner;

    public TMP_Text socreText;
    public TMP_Text highSocreText;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        IntroUI.SetActive(true);
        ItemSpawner.SetActive(false);
    }

    void Update()
    {
        if (GameManager.Instance.State == GameState.Playing)
        {
            socreText.text = "Score: " + GameManager.Instance.CalculateScore();
            highSocreText.text = "High Score: " + GameManager.Instance.HighScore;
        }
        else
        {
            socreText.text = "";
            highSocreText.text = "";
        }
    }
}
