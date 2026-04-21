using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject IntroUI;
    public GameObject ItemSpawner;

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

    }
}
