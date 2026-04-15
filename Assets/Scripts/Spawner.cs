using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("생성시간")]
    public float minSpawnTime = 1.0f;
    public float maxSpawnTime = 3.0f;

    [Header("생성할건물")]
    public GameObject[] buildingPrefabs;

    private void OnEnable()
    {
        float randomTime = Random.Range(minSpawnTime, maxSpawnTime);
        Invoke("Spawn", randomTime);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }

    void Start()
    {
        MakeInstance();
    }

    void Spawn()
    {
        Debug.Log("Spawn");
        MakeInstance();

        float randomTime = Random.Range(minSpawnTime, maxSpawnTime);
        Invoke("Spawn", randomTime);
    }

    void MakeInstance()
    {
        GameObject randomBuilding = buildingPrefabs[Random.Range(0, buildingPrefabs.Length)];
        Instantiate(randomBuilding, transform.position, Quaternion.identity);
    }
}
