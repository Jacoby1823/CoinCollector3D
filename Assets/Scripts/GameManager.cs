using UnityEngine;
using TMPro;
// Needed for TextMeshPro UI text

public class GameManager : MonoBehaviour
// This script controls health, score, game over, and winning
{
    public static GameManager instance;
    // This makes it easy for other scripts to access the GameManager

    public int playerHealth = 3;
    // Starting health for the player

    public int score = 0;
    // Starting score

    public int scoreToWin = 5;
    // The score needed to win the game

    public TextMeshProUGUI healthText;
    // Connect the HealthText UI object here

    public TextMeshProUGUI scoreText;
    // Connect the ScoreText UI object here

    public GameObject gameOverText;
    // Connect the GameOverText object here

    public GameObject winText;
    // Connect the WinText object here

    public bool isGameOver = false;
    // True if the player loses

    public bool hasWon = false;
    // True if the player wins

    void Awake()
    {
        // Make sure there is only one GameManager
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
        // Update UI when the game starts
        UpdateHealthUI();
        UpdateScoreUI();

        // Hide end screens at the start
        gameOverText.SetActive(false);
        winText.SetActive(false);
    }

    public void TakeDamage(int damageAmount)
    {
        // Do not take damage if the game is already over or won
        if (isGameOver || hasWon)
        {
            return;
        }

        // Subtract health
        playerHealth -= damageAmount;

        // Update health text
        UpdateHealthUI();

        // Check if player lost
        if (playerHealth <= 0)
        {
            GameOver();
        }
    }

    public void AddScore(int amount)
    {
        // Do not add score if the game is already over or won
        if (isGameOver || hasWon)
        {
            return;
        }

        // Add points
        score += amount;

        // Update score text
        UpdateScoreUI();

        // Check if player reached winning score
        if (score >= scoreToWin)
        {
            WinGame();
        }
    }

    void UpdateHealthUI()
    {
        // Show current health on screen
        healthText.text = "Health: " + playerHealth;
    }

    void UpdateScoreUI()
    {
        // Show current score on screen
        scoreText.text = "Score: " + score;
    }

    void GameOver()
    {
        // Mark game as lost
        isGameOver = true;

        // Show game over text
        gameOverText.SetActive(true);
    }

    void WinGame()
    {
        // Mark game as won
        hasWon = true;

        // Show win text
        winText.SetActive(true);
    }
}