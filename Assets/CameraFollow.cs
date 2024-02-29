using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 0, -10);
    public float smoothSpeed = 0.125f;

    private Vector3 screenShakeOffset = Vector3.zero;

    void FixedUpdate()
    {
        if (target != null)
        {
            // Calculate desired position with offset
            Vector3 desiredPosition = target.position + offset;

            // Smoothly interpolate towards the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Apply screen shake offset after smooth follow calculation
            transform.position = smoothedPosition + screenShakeOffset;
        }
    }

    // Method to apply screen shake offset
    public void ApplyScreenShake(Vector3 shakeOffset)
    {
        screenShakeOffset = shakeOffset;
    }

    // Method to reset screen shake offset
    public void ResetScreenShake()
    {
        screenShakeOffset = Vector3.zero;
    }
}
