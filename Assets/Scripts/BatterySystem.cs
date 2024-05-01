using UnityEngine;
using UnityEngine.UI;

public class BatterySystem : MonoBehaviour
{
    public float batteryLife = 100f;
    public float drainRate = 1f;
    public Image batteryBar;  // Drag the UI Image component here in the Inspector

    private void Update()
    {
        if (batteryLife > 0)
        {
            batteryLife -= drainRate * Time.deltaTime; // Decrease battery life
            UpdateBatteryUI();
            UpdateBatteryColor(); // Update the color based on the new battery life
        }
    }

    private void UpdateBatteryUI()
    {
        if (batteryBar != null)
        {
            batteryBar.fillAmount = batteryLife / 100f; // Update the fill amount
        }
    }

    // Method to update the color of the battery bar based on current battery level
    private void UpdateBatteryColor()
    {
        // Define color based on battery percentage
        if (batteryLife > 70)
        {
            batteryBar.color = Color.green; // Green for 70% and above
        }
        else if (batteryLife > 30)
        {
            batteryBar.color = new Color(1, 0.65f, 0, 1); // Orange for 30% to 69%
        }
        else
        {
            batteryBar.color = Color.red; // Red for below 30%
        }
    }
}
