using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    public AudioClip asteroidDestroySound; 
    private AudioSource audioSource; 

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);

            if (asteroidDestroySound != null && audioSource != null)
            {
                audioSource.PlayOneShot(asteroidDestroySound);
            }
        }
    }
}
