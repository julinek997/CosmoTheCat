using UnityEngine;

public class AsteroidCounter : MonoBehaviour
{
    private int destroyedAsteroidsCount = 0;
    public int DestroyedAsteroidsCount
    {
        get { return destroyedAsteroidsCount; }
    }
    
    public void IncrementDestroyedAsteroidsCount()
    {
        destroyedAsteroidsCount++;
    }
}
