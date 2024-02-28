using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject[] asteroidPrefabs; 
    public int numberOfAsteroids = 5;
    public float spawnRadius = 10f;
    public Vector2 spawnBounds = new Vector2(20f, 20f);
    public Vector3 scale = new Vector3(50f, 50f, 50f); 

    void Start()
    {
        SpawnAsteroids();
    }

    void SpawnAsteroids()
    {
        Vector2 spawnPoint = new Vector2(-11f, -11f);

        for (int i = 0; i < numberOfAsteroids; i++)
        {
            Vector2 randomPosition = new Vector2(Random.Range(0, spawnBounds.x), Random.Range(0, spawnBounds.y));

            randomPosition = Vector2.ClampMagnitude(randomPosition, spawnRadius);

            Vector3 spawnPosition = (Vector3)spawnPoint + new Vector3(randomPosition.x, randomPosition.y, 0f);

            GameObject asteroidPrefab = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];

            GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);

            asteroid.transform.localScale = scale;
        }
    }
}
