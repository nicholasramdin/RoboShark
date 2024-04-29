using UnityEngine;

public class EggItem : MonoBehaviour
{
    public int value = 5; // The value of eggs the player gets per collection

    private void OnTriggerEnter(Collider other)
    {
        RobotSharkController player = other.GetComponent<RobotSharkController>();
        if (player != null)
        {
            // Call the method to collect eggs, pass in the value
            player.CollectEggs(value);
            // You may want to deactivate the egg object here if it should disappear on collection
            gameObject.SetActive(false);
        }
    }
}
