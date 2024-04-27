using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; 
    public TextMeshProUGUI scoreText; 

    private int score = 0; 

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int points)
    {
        score += points; 
        UpdateScoreText(); 
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
        else
        {
            Debug.LogError("ScoreText reference is null. Make sure to assign the ScoreText GameObject with TextMeshProUGUI component to the ScoreManager script.");
        }
    }
}
