using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour
{
    public float minSpeed = 1f;
    public float maxSpeed = 5f;

    void Start()
    {
        float speed = Random.Range(minSpeed, maxSpeed);
        Vector2 direction = Random.insideUnitCircle.normalized;
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
