using UnityEngine;

public class EggItem : MonoBehaviour
{
    public int value = 5; // The value of eggs the player gets per collection

    private void OnTriggerEnter(Collider other)
    {
        RobotSharkController player = other.GetComponent<RobotSharkController>();

        if (player != null)
        {
            // Attempt to collect eggs. If the player is at max capacity, the method will return false.
            bool collected = player.CollectEggs(value);

            // Only deactivate the egg object if collected
            if (collected)
            {
                gameObject.SetActive(false);
            }
            else
            {
                // Provide feedback to the player that they can't collect more eggs
                // This could be a UI message, a sound, etc.
                Debug.Log("You can't carry more eggs! Go to the deposit point.");
            }
        }
    }
}
