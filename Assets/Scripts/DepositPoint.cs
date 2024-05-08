using UnityEngine;

public class DepositPoint : MonoBehaviour
{
    public EnemySpawner spawner; // Assign this in the Inspector

    private void OnTriggerEnter(Collider other)
    {
        RobotSharkController player = other.GetComponent<RobotSharkController>();
        if (player != null && player.GetCurrentEggs() > 0)
        {
            player.DepositEggs();
            spawner.SpawnEnemy();  // Call to spawn an enemy
            GameManager.Instance.ResetEggs(); // Reset eggs in your game logic
        }
    }
}
