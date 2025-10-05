//using UnityEditor.Callbacks;
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
            //GameObject meteor = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
            //Destroy(meteor, maxTimelife);

            //Para pooling
            //GameObject meteor = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
            GameObject meteor = ObjectPooling.Instance.GetObject(asteroidPrefab);
            meteor.transform.position = spawnPosition;
            meteor.transform.rotation = Quaternion.identity;
            Rigidbody rb = meteor.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero; // Reiniciar la velocidad
                rb.angularVelocity = Vector3.zero; // Reiniciar la velocidad angular
            }
            //Destroy(meteor, maxTimelife);
            //meteor2.transform.position = spawnPosition;
            //meteor2.transform.rotation = Quaternion.identity;
            //Rigidbody rb = meteor2.GetComponent<Rigidbody>();//rb = meteor2.AddComponent<Rigidbody>();
        }
    }

    }


