using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAsteroidSpawner : MonoBehaviour
{
    [Header("Spawning")]
    public GameObject asteroidPrefab;
    public int numberOfAsteroids = 5;
    public int maxAsteroids = 10;
    public float spawnRadius = 8f;
    public float checkInterval = 2f;

    private List<GameObject> activeAsteroids = new List<GameObject>();
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        SpawnAsteroids();
        StartCoroutine(CheckAndRespawnAsteroids());
    }

    void SpawnAsteroids()
    {
        for (int i = 0; i < numberOfAsteroids; i++)
        {
            SpawnOneAsteroid();
        }
    }
    public void NotifyAsteroidDestroyed(GameObject asteroid)
    {
        activeAsteroids.Remove(asteroid);
    }
    public void SpawnOneAsteroid()
    {
        if (player == null) return;

        Vector2 playerPos = player.position;
        Vector2 randomPos = playerPos + Random.insideUnitCircle.normalized * spawnRadius;

        if (Vector2.Distance(randomPos, playerPos) < 3f)
        {
            randomPos = playerPos + (randomPos - playerPos).normalized * 3f;
        }

        GameObject asteroid = Instantiate(asteroidPrefab, randomPos, Quaternion.identity);
        activeAsteroids.Add(asteroid);

        // Optional: random scale
        float randomScale = Random.Range(0.8f, 1.5f);
        asteroid.transform.localScale = Vector3.one * randomScale;
    }

    IEnumerator CheckAndRespawnAsteroids()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval);

            activeAsteroids.RemoveAll(a => a == null);

            if (activeAsteroids.Count < maxAsteroids)
            {
                SpawnOneAsteroid();
            }
        }
    }
}
