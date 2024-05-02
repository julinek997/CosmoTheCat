using System.Collections;
using UnityEngine;

public class DoubleSpeedItem : MonoBehaviour
{
    public PlayerShipController playerShipController;
    public float duration = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateDoubleSpeed();
            Destroy(gameObject);
        }
    }

    private void ActivateDoubleSpeed()
    {
        playerShipController.EnableDoubleSpeed(duration);
    }
}
