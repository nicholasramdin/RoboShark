using UnityEngine;

public class DepositPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        RobotSharkController player = other.GetComponent<RobotSharkController>();
        if (player != null && player.GetCurrentEggs() > 0) // Ensure player has eggs to deposit
        {
            player.DepositEggs();
        }
    }
}

