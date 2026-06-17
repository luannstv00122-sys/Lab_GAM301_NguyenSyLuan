using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public TextMeshProUGUI scoreText;

    private int score;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        score = PlayerPrefs.GetInt("Score");
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        score += amount;

        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.Save();

        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }
    public void ResetScore()
{
    score = 0;

    PlayerPrefs.SetInt("Score", 0);
    PlayerPrefs.Save();

    UpdateScoreUI();
}
}