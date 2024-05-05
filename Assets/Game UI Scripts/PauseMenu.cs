using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuPanel;

    private bool isPaused = false;

    void Start()
    {
        pauseMenuPanel.SetActive(false);
    }

 void Update()
    {
        Debug.Log("Update method of PauseMenu script is called.");
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePauseMenu();
        }
    }

    void TogglePauseMenu()
    {
        Debug.Log("Before toggling, isPaused is: " + isPaused);
        isPaused = !isPaused;

        Debug.Log("After toggling, isPaused is: " + isPaused);
        pauseMenuPanel.SetActive(isPaused);

        if (isPaused)
        {
            Debug.Log("Pause menu panel is activated.");
        }
        else
        {
            Debug.Log("Pause menu panel is deactivated.");
        }
        
        Time.timeScale = isPaused ? 0 : 1;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
    }

    public void SaveGame()
    {
    }
}
