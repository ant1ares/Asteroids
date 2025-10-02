using UnityEngine;

public class EnemyFragment : MonoBehaviour
{
    public GameObject fragmentPrefab;
    public int numberOfFragments = 2;
    public float fragmentSpeed = 5f;
    public float maxLifeTime = 2f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Meteor fragmentado!");
            //Instantiate(fragmentPrefab, transform.position, transform.rotation);
            SpawnFragments();
            Destroy(gameObject);
        }
    }

    private void SpawnFragments()
    {
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
    }
}
