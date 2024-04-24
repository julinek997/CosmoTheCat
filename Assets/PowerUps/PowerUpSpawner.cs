using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{

    public GameObject tripleShotItemPrefab;
    public float spawnDelay = 10f;
    public Vector2 spawnAreaSize = new Vector2(10f, 10f);

    void Start()
    {
        StartCoroutine(SpawnTripleShotItem());
    }

    IEnumerator SpawnTripleShotItem()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);

            Vector3 spawnPosition = GetRandomSpawnPosition();
            Instantiate(tripleShotItemPrefab, spawnPosition, Quaternion.identity);
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        float randomX = Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f);
        float randomY = Random.Range(-spawnAreaSize.y / 2f, spawnAreaSize.y / 2f);
        return transform.position + new Vector3(randomX, randomY, 0f);
    }
}

