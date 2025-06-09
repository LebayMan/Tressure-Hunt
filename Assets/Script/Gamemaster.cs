using UnityEngine;
using TMPro;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public SimplePlayerUI playerUI;

    private void Awake()
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

    private void Start()
    {
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        playerUI.AddCoin(amount);
        UpdateScoreUI();
    }
    public void Won()
    {
        playerUI.Winpanel.SetActive(true);
        playerUI.ScorePanel.SetActive(false);
    }
    public void Lose()
    {
        playerUI.Losepanel.SetActive(true);
        playerUI.ScorePanel.SetActive(false);
    }
    public void deltatime()
    {
        Time.timeScale = 1f; // Pause the game
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
    public void OnApplicationQuit()
    {
        Application.Quit();
    }
    public void Resetgame()
    {
        playerUI.Losepanel.SetActive(false);
        playerUI.Winpanel.SetActive(false);
        playerUI.ScorePanel.SetActive(true);
        playerUI.AddHealth(playerUI.maxHealth);
        playerUI.AddExp(0f);
        playerUI.AddCoin(0);
        score = 0;
        UpdateScoreUI();
    }
}
