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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
