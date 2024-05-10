using UnityEngine;
using UnityEngine.UI;

public class BatterySystem : MonoBehaviour
{
    public float batteryLife = 100f;
    public float drainRate = 1f;
    public float maxBattery = 100f; // Define the maximum battery life
    public Image batteryBarFill;

    private void Update()
    {
        if (batteryLife > 0)
        {
            batteryLife -= drainRate * Time.deltaTime;
            UpdateBatteryUI();
            if (batteryLife <= 0)
            {
                GameManager.Instance.HandleGameOver();
            }
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

    public void ReplenishBatteryToFull()
    {
        batteryLife = maxBattery;
        UpdateBatteryUI();
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
}
