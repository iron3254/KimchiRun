using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("건물 생성 설정")]
    public float minBuildingSpawnTime = 1.0f;
    public float maxBuildingSpawnTime = 3.0f;
    public GameObject[] buildingPrefabs;

    [Header("적 생성 설정")]
    public float minEnemySpawnTime = 2.0f;
    public float maxEnemySpawnTime = 5.0f;
    public GameObject[] enemyPrefabs;

    private void OnEnable()
    {
        // 건물 생성 타이머 시작
        float randomBuildingTime = Random.Range(minBuildingSpawnTime, maxBuildingSpawnTime);
        Invoke("SpawnBuilding", randomBuildingTime);

        // 적 생성 타이머 시작
        float randomEnemyTime = Random.Range(minEnemySpawnTime, maxEnemySpawnTime);
        Invoke("SpawnEnemy", randomEnemyTime);
    }
    
    private void OnDisable()
    {
        CancelInvoke();
    }

    void Start()
    {
        MakeBuildingInstance();
        // 게임 시작 시 적도 하나 생성할지 여부에 따라 주석을 활성화할 수 있습니다.
        // MakeEnemyInstance(); 
    }

    void SpawnBuilding()
    {
        MakeBuildingInstance();

        float randomTime = Random.Range(minBuildingSpawnTime, maxBuildingSpawnTime);
        Invoke("SpawnBuilding", randomTime);
    }

    void SpawnEnemy()
    {
        MakeEnemyInstance();

        float randomTime = Random.Range(minEnemySpawnTime, maxEnemySpawnTime);
        Invoke("SpawnEnemy", randomTime);
    }

    void MakeBuildingInstance()
    {
        if (GameManager.Instance != null && GameManager.Instance.State != GameState.Playing) return;

        if (buildingPrefabs != null && buildingPrefabs.Length > 0)
        {
            GameObject randomBuilding = buildingPrefabs[Random.Range(0, buildingPrefabs.Length)];
            Instantiate(randomBuilding, transform.position, Quaternion.identity);
        }
    }

    void MakeEnemyInstance()
    {
        if (GameManager.Instance != null && GameManager.Instance.State != GameState.Playing) return;

        if (enemyPrefabs != null && enemyPrefabs.Length > 0)
        {
            GameObject randomEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Instantiate(randomEnemy, transform.position, Quaternion.identity);
        }
    }
}
