using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreenManager : MonoBehaviour
{
    public GameObject loseScreenPanel;

    void Start()
    {
        // Initially hide the lose screen
        loseScreenPanel.SetActive(false);
    }

    public void ShowLoseScreen()
    {
        // Show the lose screen and freeze the game
        loseScreenPanel.SetActive(true);
        Time.timeScale = 0f;  // Freezes the game
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Unfreeze the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reloads the current scene
    }

    public void ExitGame()
    {
        Application.Quit(); // Exits the game
    }
}
