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
            Vector3 desiredPosition = target.position + offset;

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            transform.position = smoothedPosition + screenShakeOffset;
        }
    }

    public void ApplyScreenShake(Vector3 shakeOffset)
    {
        screenShakeOffset = shakeOffset;
    }

    public void ResetScreenShake()
    {
        screenShakeOffset = Vector3.zero;
    }
}
