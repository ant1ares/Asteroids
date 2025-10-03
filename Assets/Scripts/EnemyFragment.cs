using System;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class EnemyFragment : MonoBehaviour
{
    public GameObject fragmentPrefab;
    public int numberOfFragments = 2;
    public float fragmentSpeed = 5f;
    public float maxLifeTime = 2f;
    public float spreadAngle = 30f;
    public Boolean canFragment = true;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            Debug.Log("Meteor fragmentado!");
            //Instantiate(fragmentPrefab, transform.position, transform.rotation);
            //SpawnFragments(bulletDir);
            if (canFragment)
            {
                Vector3 bulletDir = bullet.targetVector.normalized;
                SpawnFragments(bulletDir);
            }
            Destroy(gameObject);
        }
    }

    private void SpawnFragments(Vector3 bulletDir)
    {
        /*
        for (int i = 0; i < numberOfFragments; i++)
        {
            GameObject fragmentos = Instantiate(fragmentPrefab, transform.position, transform.rotation);
            Rigidbody rb = fragmentos.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = fragmentos.AddComponent<Rigidbody>();
            }
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            rb.AddForce(randomDirection * fragmentSpeed, ForceMode.Impulse);
            Destroy(fragmentos, maxLifeTime);
        }
        */
        // rotaciones simetricas
        bulletDir.Normalize();
        Quaternion rot1 = Quaternion.AngleAxis(spreadAngle, Vector3.back);
        Quaternion rot2 = Quaternion.AngleAxis(-spreadAngle, Vector3.back);

        Vector3 dir1 = rot1 * bulletDir;
        Vector3 dir2 = rot2 * bulletDir;

        CreateFragment(-dir1);
        CreateFragment(-dir2);
    }

    private void CreateFragment(Vector3 direction)
    {
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
        GameObject fragmentos = Instantiate(fragmentPrefab, transform.position, rotation);
        Rigidbody rb = fragmentos.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = fragmentos.AddComponent<Rigidbody>();
        }
        rb.AddForce(-direction * fragmentSpeed, ForceMode.Impulse);
        // evitar fragmentacion infinita
        EnemyFragment ef = fragmentos.GetComponent<EnemyFragment>();
        if (ef != null)
        {
            ef.canFragment = false;
        }
        Destroy(fragmentos, maxLifeTime);
    }
    

}
