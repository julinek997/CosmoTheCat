using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour
{
    public AudioClip destructionSound; 
    private AudioSource audioSource; 

    public float minSpeed = 1f;
    public float maxSpeed = 5f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        float speed = Random.Range(minSpeed, maxSpeed);
        Vector2 direction = Random.insideUnitCircle.normalized;
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (destructionSound != null && audioSource != null)
            {
                
                if (!audioSource.enabled)
            {
                audioSource.enabled = true;
            }

                audioSource.PlayOneShot(destructionSound);
            }

            Destroy(gameObject);
        }
    }
}
