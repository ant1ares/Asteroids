using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public float spawnRatePerMinute = 30f;
    public float spawnRateIncrement = 1f;
    public float xlimit;
    public float maxTimelife = 2f;

    private float spawnNext = 0;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > spawnNext)
        {
            //SpawnAsteroid();
            spawnNext = Time.time + 60 / spawnRatePerMinute;
            spawnRatePerMinute += spawnRateIncrement;
            float rand = Random.Range(-xlimit, xlimit);
            Vector2 spawnPosition = new Vector2(rand, 7f);
            GameObject meteor = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
            Destroy(meteor, maxTimelife);
        }
    }
/*
    private void SpawnAsteroid()
    {
        Instantiate(asteroidPrefab, transform.position, Quaternion.identity);
    }
    */
    }


