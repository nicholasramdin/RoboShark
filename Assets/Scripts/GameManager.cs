using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton pattern

    private List<GameObject> eggs = new List<GameObject>(); // List to store all eggs

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
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
            egg.SetActive(true); // Reactivate all eggs
        }
    }
}
