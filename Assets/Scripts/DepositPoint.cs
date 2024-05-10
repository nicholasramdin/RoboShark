using UnityEngine;

public class DepositPoint : MonoBehaviour
{
    public EnemySpawner spawner; // Assign this in the Inspector
    public AudioSource depositAudioSource; // Assign in Inspector

    private void OnTriggerEnter(Collider other)
    {
        RobotSharkController player = other.GetComponent<RobotSharkController>();
        if (player != null && player.GetCurrentEggs() > 0)
        {
            player.DepositEggs();
            depositAudioSource.Play();  // Play sound
            spawner.SpawnEnemy();  // Call to spawn an enemy
            GameManager.Instance.ResetEggs(); // Reset eggs in game logic
        }
        GameManager.Instance.ReplenishBattery(); // Replenish battery on player contact
    }
}
