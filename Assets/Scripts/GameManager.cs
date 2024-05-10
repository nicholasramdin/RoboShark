using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton pattern
    public BatterySystem batterySystem; // Assign this in the Inspector
    private List<GameObject> eggs = new List<GameObject>(); // List to store all eggs
    public GameObject loseScreenPanel; // Make sure this is assigned in the inspector

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Re-find and set the loseScreenPanel if necessary, especially if it's a scene-specific UI element
        if (loseScreenPanel == null)
        {
            loseScreenPanel = GameObject.Find("loseScreenPanel");
        }
    }

    public void RegisterEgg(GameObject egg)
    {
        if (!eggs.Contains(egg))
        {
            eggs.Add(egg);
        }
    }

    public void ResetEggs()
    {
        foreach (GameObject egg in eggs)
        {
            if (egg != null) egg.SetActive(true); // Reactivate all eggs
        }
    }

    // Called by DepositPoint when the player contacts it
    public void ReplenishBattery()
    {
        if (batterySystem != null)
        {
            batterySystem.ReplenishBatteryToFull();
        }
    }

    public void HandleGameOver()
    {
        if (loseScreenPanel != null)
        {
            loseScreenPanel.SetActive(true); // Show the lose screen
            Time.timeScale = 0; // Freeze the game
        }
        else
        {
            Debug.LogError("LoseScreenPanel is missing");
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1; // Ensure the game is not frozen
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
    }

    public void ExitGame()
    {
        Application.Quit(); // Exit the game
    }
}
