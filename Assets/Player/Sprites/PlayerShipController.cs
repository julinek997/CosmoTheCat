using UnityEngine;

public class PlayerShipController : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 200f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Vector3 startingPosition = new Vector3(0, 0, 0); 
    public float bulletForce = 20f;
    public float bulletLifetime = 3f;
    public AudioClip shootSound; 
    private AudioSource audioSource;

    void Start()
    {
        transform.position = startingPosition;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Vertical");
        Vector2 movement = transform.up * moveInput * speed * Time.deltaTime;
        transform.Translate(movement);

        float rotateInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.back * rotateInput * rotationSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            Debug.Log("Collision with asteroid detected");
            Destroy(gameObject);
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(bullet.transform.up * bulletForce, ForceMode2D.Impulse);
        }
        else
        {
            Debug.LogWarning("Bullet prefab is missing Rigidbody2D component.");
        }
        
        Destroy(bullet, bulletLifetime);
        
        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }
}
