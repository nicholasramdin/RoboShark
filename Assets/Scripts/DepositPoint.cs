using UnityEngine;

public class DepositPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        RobotSharkController player = other.GetComponent<RobotSharkController>();
        if (player != null)
        {
            // Call the deposit method from the player script
            player.DepositEggs();
        }
    }
}
