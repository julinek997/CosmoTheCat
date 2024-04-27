using UnityEngine;
using UnityEngine.SceneManagement;
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
    public AudioClip powerUpSound;
    private AudioSource audioSource;
    private bool hasTripleShot = false;
    public int powerUpPoints = 5;
    private float tripleShotDuration = 0f;
    public ScoreManager scoreManager;

 void Start()
    {
        Debug.Log("ScoreManager reference: " + scoreManager);
        FindValidSpawnPosition();
        audioSource = GetComponent<AudioSource>();

        if (FindObjectOfType<DefeatScreenManager>() == null)
        {
            GameObject defeatScreenManagerObj = new GameObject("DefeatScreenManager");
            defeatScreenManagerObj.AddComponent<DefeatScreenManager>();
        }
    }

void FindValidSpawnPosition()
    {
        Collider2D[] obstacles = Physics2D.OverlapCircleAll(transform.position, 5f, LayerMask.GetMask("Asteroid", "PowerUp"));
        Vector3 spawnPosition = Vector3.zero;
        bool validSpawn = false;

        while (!validSpawn)
        {
            float randomX = Random.Range(-10f, 10f);
            float randomY = Random.Range(-10f, 10f);
            spawnPosition = new Vector3(randomX, randomY, 0f);

            validSpawn = true;
            foreach (Collider2D obstacle in obstacles)
            {
                if (Vector3.Distance(spawnPosition, obstacle.transform.position) < 2f)
                {
                    validSpawn = false;
                    break;
                }
            }
        }

        transform.position = spawnPosition;
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
        if (hasTripleShot)
        {
            tripleShotDuration -= Time.deltaTime;
            if (tripleShotDuration <= 0f)
            {
                DisableTripleShot();
            }
        }
    }

void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("PowerUp"))
    {
        Debug.Log("PowerUp detected");
        EnableTripleShot(); 
        if (powerUpSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(powerUpSound);
        }
        Destroy(collision.gameObject); 

        if (scoreManager != null)
        {
            Debug.Log("Adding points");
            scoreManager.AddScore(powerUpPoints); 
        }
        else
        {
            Debug.LogError("ScoreManager reference is null");
        }
    }
    else if (collision.gameObject.CompareTag("Asteroid"))
    {
        Debug.Log("Collision with asteroid detected");
        Destroy(gameObject);
        SceneManager.LoadScene(2);
    }
}

 void Shoot()
    {
        if (hasTripleShot)
        {
            GameObject bullet1 = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            GameObject bullet2 = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0, 0, 15f));
            GameObject bullet3 = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0, 0, -15f));

            Rigidbody2D rb1 = bullet1.GetComponent<Rigidbody2D>();
            Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
            Rigidbody2D rb3 = bullet3.GetComponent<Rigidbody2D>();

            if (rb1 != null && rb2 != null && rb3 != null)
            {
                rb1.AddForce(bullet1.transform.up * bulletForce, ForceMode2D.Impulse);
                rb2.AddForce(bullet2.transform.up * bulletForce, ForceMode2D.Impulse);
                rb3.AddForce(bullet3.transform.up * bulletForce, ForceMode2D.Impulse);
            }
            else
            {
                Debug.LogWarning("Bullet prefab is missing Rigidbody2D component.");
            }

            Destroy(bullet1, bulletLifetime);
            Destroy(bullet2, bulletLifetime);
            Destroy(bullet3, bulletLifetime);
        }
        else
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
        }

        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }
    public void EnableTripleShot()
    {
        hasTripleShot = true;
        tripleShotDuration = 10f;
    }
    void DisableTripleShot()
    {
        hasTripleShot = false;
    }
}
