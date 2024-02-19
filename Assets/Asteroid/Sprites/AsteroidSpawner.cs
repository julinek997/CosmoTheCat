using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject[] asteroidPrefabs; // Array to hold multiple asteroid prefabs
    public int numberOfAsteroids = 5;
    public float spawnRadius = 10f;

    void Start()
    {
        SpawnAsteroids();
    }

    void SpawnAsteroids()
    {
        for (int i = 0; i < numberOfAsteroids; i++)
        {
            // Generate a random position within the spawn radius
            Vector2 randomPosition = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPosition = transform.position + new Vector3(randomPosition.x, randomPosition.y, 0f);

            // Randomly choose an asteroid prefab from the array
            GameObject asteroidPrefab = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];

            // Instantiate the asteroid prefab at the random position
            Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
