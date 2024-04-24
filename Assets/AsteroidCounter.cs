using UnityEngine;
using UnityEngine.UI;

public class AsteroidCounter : MonoBehaviour
{
    private int destroyedAsteroids = 0;
    public Text counterText; 

    public void IncrementDestroyedAsteroids()
    {
        destroyedAsteroids++;
        UpdateCounterText();
    }

    public int GetDestroyedAsteroidsCount()
    {
        return destroyedAsteroids;
    }

    private void UpdateCounterText()
    {
        if (counterText != null)
        {
            counterText.text = "Destroyed Asteroids: " + destroyedAsteroids;
        }
    }
}
