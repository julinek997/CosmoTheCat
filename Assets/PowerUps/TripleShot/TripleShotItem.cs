using UnityEngine;

public class TripleShotItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Triple Shot Power-up picked up");
            Destroy(gameObject); 
        }
    }
}