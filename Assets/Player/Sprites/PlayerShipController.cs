using UnityEngine;

public class PlayerShipController : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 200f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    void Update()
    {
        float moveInput = Input.GetAxis("Vertical");
        Vector2 movement = transform.up * moveInput * speed * Time.deltaTime;
        transform.Translate(movement);

        float rotateInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.back * rotateInput * rotationSpeed * Time.deltaTime);

        CheckBounds();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void CheckBounds()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        if (viewportPosition.x < 0) 
        {
            transform.position = Camera.main.ViewportToWorldPoint(new Vector3(1.1f, viewportPosition.y, 0));
        }
        else if (viewportPosition.x > 1) 
        {
            transform.position = Camera.main.ViewportToWorldPoint(new Vector3(-0.1f, viewportPosition.y, 0));
        }

        if (viewportPosition.y < 0) 
        {
            transform.position = Camera.main.ViewportToWorldPoint(new Vector3(viewportPosition.x, 1.1f, 0));
        }
        else if (viewportPosition.y > 1) 
        {
            transform.position = Camera.main.ViewportToWorldPoint(new Vector3(viewportPosition.x, -0.1f, 0));
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}