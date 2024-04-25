using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatScreenManager : MonoBehaviour
{
    public GameObject defeatScreen;

    void Start()
    {
        if (defeatScreen != null)
        {
            defeatScreen.SetActive(false);
        }
    }

    public void ShowDefeatScreen()
    {
        if (defeatScreen != null)
        {
            defeatScreen.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}