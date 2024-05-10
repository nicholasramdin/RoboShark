using UnityEngine;
using UnityEngine.UI; // Needed for accessing Button components
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton pattern
    public BatterySystem batterySystem; // Assign in inspector
    private List<GameObject> eggs = new List<GameObject>(); // List to store all eggs
    public GameObject loseScreenPanel; // Assign in inspector

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
        loseScreenPanel = GameObject.Find("loseScreenPanel");
        if (loseScreenPanel == null)
        {
            Debug.LogError("LoseScreenPanel not found after scene load");
        }
        else
        {
            SetupButtons();
            loseScreenPanel.SetActive(false); // Ensure it's initially hidden
        }
        Time.timeScale = 1; // Reset time scale on scene load to ensure game isn't frozen
        Debug.Log("Scene loaded, time scale reset.");
    }

    void SetupButtons()
    {
        // Set up buttons again after the scene is loaded
        Button restartButton = loseScreenPanel.transform.Find("RestartButton").GetComponent<Button>();
        Button exitButton = loseScreenPanel.transform.Find("ExitButton").GetComponent<Button>();

        if (restartButton != null && exitButton != null)
        {
            restartButton.onClick.RemoveAllListeners();
            restartButton.onClick.AddListener(RestartGame);

            exitButton.onClick.RemoveAllListeners();
            exitButton.onClick.AddListener(ExitGame);
        }
        else
        {
            Debug.LogError("One or more buttons are not found in the LoseScreenPanel.");
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
            Debug.Log("Game Over, screen frozen.");
        }
        else
        {
            Debug.LogError("LoseScreenPanel is missing");
        }
    }

    public void RestartGame()
    {
        Debug.Log("Restarting Game");
        Time.timeScale = 1; // Reset the time scale to 1 to ensure the game isn't frozen
        SceneManager.LoadScene("mainScene"); // Reload the main scene directly
        Debug.Log("Game restarted, main scene loaded.");
    }

    public void ExitGame()
    {
        Debug.Log("Attempting to exit game...");
        Application.Quit(); // Exit the game
    }
}
