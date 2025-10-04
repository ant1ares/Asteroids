using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float maxLifeTime = 3f;
    public Vector3 targetVector;
    //public GameObject fragmentosPrefab;
    void OnEnable()
    {
        Invoke(nameof(Deactivate), maxLifeTime);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Deactivate()
    {
        CancelInvoke();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * targetVector * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            IncreaseScore();
            //Instantiate(fragmentosPrefab, transform.position, transform.rotation);
            //Destroy(collision.gameObject);
            //gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
            Deactivate();
        }
    }

    private void IncreaseScore()
    {
        player.SCORE++;
        Debug.Log(player.SCORE);
        UpdateScoreText();
    }
    private void UpdateScoreText()
    {
        GameObject go = GameObject.FindGameObjectWithTag("UI");
        go.GetComponent<Text>().text = "Puntos : " + player.SCORE;
    }
}
