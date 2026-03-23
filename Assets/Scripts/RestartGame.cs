using UnityEngine;
using UnityEngine.SceneManagement;
// Needed to reload the scene

public class RestartGame : MonoBehaviour
// This script lets the player press R to restart after losing or winning
{
    void Update()
    {
        // Make sure GameManager exists
        if (GameManager.instance == null)
        {
            return;
        }

        // Check if player lost or won
        if (GameManager.instance.isGameOver || GameManager.instance.hasWon)
        {
            // Restart if player presses R
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}