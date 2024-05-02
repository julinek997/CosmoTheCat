using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerShipController : MonoBehaviour
{
    public float speed = 5f;
    private float normalSpeed;
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
    private bool hasDoubleSpeed = false;
    public int powerUpPoints = 5;
    private float tripleShotDuration = 0f;
    private float doubleSpeedDuration = 0f;
    public ScoreManager scoreManager;
    public DoubleSpeedItem doubleSpeedItem; // Add this field

    void Start()
    {
        Debug.Log("ScoreManager reference: " + scoreManager);
        FindValidSpawnPosition();
        audioSource = GetComponent<AudioSource>();
        normalSpeed = speed;

        if (FindObjectOfType<DefeatScreenManager>() == null)
        {
            GameObject defeatScreenManagerObj = new GameObject("DefeatScreenManager");
            defeatScreenManagerObj.AddComponent<DefeatScreenManager>();
        }
        doubleSpeedItem = FindObjectOfType<DoubleSpeedItem>();
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

        if (hasDoubleSpeed)
        {
            doubleSpeedDuration -= Time.deltaTime;
            if (doubleSpeedDuration <= 0f)
            {
                DisableDoubleSpeed();
            }
        }
    }

 void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("PowerUp"))
    {
        Debug.Log("Power-up Collision detected");

        if (powerUpSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(powerUpSound);
        }
        Destroy(collision.gameObject);

        if (collision.gameObject.GetComponent<TripleShotItem>() != null)
        {
            Debug.Log("Triple Shot Power-up picked up");
            EnableTripleShot();
        }
        else if (collision.gameObject.GetComponent<DoubleSpeedItem>() != null)
        {
            Debug.Log("Double Speed Power-up picked up");
            EnableDoubleSpeed(doubleSpeedItem.duration); 
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
        Debug.Log("Triple Shot Enabled: " + hasTripleShot);
    }

    void DisableTripleShot()
    {
        hasTripleShot = false;
    }
    
    public void EnableDoubleSpeed(float duration)
    {
        hasDoubleSpeed = true;
        doubleSpeedDuration = duration;
        speed *= 2f; // Double the speed
        StartCoroutine(DisableDoubleSpeedAfter(duration));
    }

    IEnumerator DisableDoubleSpeedAfter(float duration)
    {
        yield return new WaitForSeconds(duration);
        DisableDoubleSpeed();
    }

    void DisableDoubleSpeed()
    {
        hasDoubleSpeed = false;
        speed = normalSpeed; 
    }
}
