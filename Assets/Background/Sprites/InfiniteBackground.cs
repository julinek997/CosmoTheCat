using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    public Transform player; 
    public float scrollSpeed = 0.1f;

    private Rigidbody2D playerRigidbody;
    private Vector3 previousPlayerPosition;

    void Start()
    {
        playerRigidbody = player.GetComponent<Rigidbody2D>();

        previousPlayerPosition = player.position;
    }

    void Update()
    {
        Vector3 playerMovement = player.position - previousPlayerPosition;

        Vector3 backgroundMovement = -playerMovement * scrollSpeed; 

        transform.position += backgroundMovement;

        previousPlayerPosition = player.position;
    }
}
