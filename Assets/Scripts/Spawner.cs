using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject slimePrefab;
    public float spawnInterval = 2f;
    public float spawnRange = 5f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnSlime();
            timer = 0f;
        }
    }

    void SpawnSlime()
    {
        Vector3 randomPos = new Vector3(
            Random.Range(-spawnRange, spawnRange),
            1f,
            Random.Range(-spawnRange, spawnRange)
        );

        Instantiate(slimePrefab, randomPos, Quaternion.identity);
    }
}
