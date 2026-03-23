using UnityEngine;
using UnityEngine.SceneManagement;
// Needed to switch scenes

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Loads your game scene
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        // Shows in console (since quit doesn't work in editor)

        Application.Quit();
        // Works when game is built
    }
}