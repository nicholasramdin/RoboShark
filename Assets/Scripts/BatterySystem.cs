using UnityEngine;
using UnityEngine.UI;

public class BatterySystem : MonoBehaviour
{
    public float batteryLife = 100f;
    public float drainRate = 1f;
    public Image batteryBarFill;  // Assign the child Image here in the inspector

    private void Update()
    {
        if (batteryLife > 0)
        {
            batteryLife -= drainRate * Time.deltaTime;
            UpdateBatteryUI();
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
}
