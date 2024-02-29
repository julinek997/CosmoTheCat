using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid"))
        {
            Destroy(collision.gameObject);
            
            GameObject counterObject = GameObject.FindWithTag("AsteroidCounter");
            if (counterObject != null)
            {
                AsteroidCounter counter = counterObject.GetComponent<AsteroidCounter>();
                if (counter != null)
                {
                    counter.IncrementDestroyedAsteroidsCount();
                }
            }
            Destroy(gameObject);
        }
    }
}
