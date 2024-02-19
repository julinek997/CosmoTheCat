using UnityEngine;

public class PlayerShipController : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 200f;
    public GameObject bulletPrefab;
    public Transform firePoint;

private void Update()
{
    float moveInput = Input.GetAxis("Vertical");
    Vector2 movement = transform.up * moveInput * speed * Time.deltaTime;
    transform.Translate(movement);

    float rotateInput = Input.GetAxis("Horizontal");
    transform.Rotate(Vector3.back * rotateInput * rotationSpeed * Time.deltaTime);

    // Check if the player is out of screen bounds
    CheckBounds();

    if (Input.GetKeyDown(KeyCode.Space))
    {
        Shoot();
    }
}

void CheckBounds()
{
    Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
    if (viewportPosition.y < 0) 
    {
        Vector3 newPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1.1f, 0));
        transform.position = newPosition;
    }
}

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
