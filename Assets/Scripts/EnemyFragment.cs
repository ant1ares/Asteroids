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
            //Destroy(gameObject);
            Deactivate();
        }
    }

    private void SpawnFragments(Vector3 bulletDir)
    {
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
        GameObject fragment = ObjectPooling.Instance.GetObject(this.fragmentPrefab);
        
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
        fragment.transform.position = transform.position;
        fragment.transform.rotation = rotation;
        Rigidbody rb = fragment.GetComponent<Rigidbody>();

        if (rb == null)
            rb = fragment.AddComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(-direction * fragmentSpeed, ForceMode.Impulse);
        // evitar fragmentacion infinita
        EnemyFragment ef = fragment.GetComponent<EnemyFragment>();
        if (ef != null)
            ef.canFragment = false;
        fragment.SetActive(true);
    }
    void Deactivate()
    {
        gameObject.SetActive(false);
    }

}
