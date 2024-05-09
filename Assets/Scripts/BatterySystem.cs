using UnityEngine;
using UnityEngine.UI;

public class BatterySystem : MonoBehaviour
{
    public float batteryLife = 100f;
    public float drainRate = 1f;
    public Image batteryBarFill;  // Assign the child Image here in the inspector
    public GameObject loseScreenPanel;  // Assign the lose screen panel in the inspector

    private void Start()
    {
        loseScreenPanel.SetActive(false);  // Ensure the lose screen is hidden initially
    }

    private void Update()
    {
        if (batteryLife > 0)
        {
            batteryLife -= drainRate * Time.deltaTime;
            UpdateBatteryUI();
        }
        else
        {
            TriggerLoseCondition();
        }
    }

    private void UpdateBatteryUI()
    {
        if (batteryBarFill != null)
        {
            batteryBarFill.fillAmount = batteryLife / 100f;
            UpdateBatteryColor();
        }
    }

    private void UpdateBatteryColor()
    {
        if (batteryLife > 70)
            batteryBarFill.color = Color.green;
        else if (batteryLife > 30)
            batteryBarFill.color = Color.yellow;
        else
            batteryBarFill.color = Color.red;
    }

    private void TriggerLoseCondition()
    {
        loseScreenPanel.SetActive(true);  // Show the lose screen
        Time.timeScale = 0;  // Freeze the game
    }
}
