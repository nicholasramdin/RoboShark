using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemySharkPrefab; // Assign the prefab through the Inspector
    public Transform spawnPoint;        // Optional: Assign a spawn point in the inspector
    public Transform[] waypoints;       // Assign the waypoints that each enemy should follow

    public void SpawnEnemy()
    {
        if (enemySharkPrefab != null && waypoints.Length > 0)
        {
            GameObject enemy = Instantiate(enemySharkPrefab, spawnPoint.position, Quaternion.identity);
            PatrolRoute patrolRoute = enemy.GetComponent<PatrolRoute>();

            if (patrolRoute != null)
            {
                patrolRoute.waypoints = new Transform[waypoints.Length];
                waypoints.CopyTo(patrolRoute.waypoints, 0);
            }
            else
            {
                Debug.LogError("PatrolRoute component not found on the spawned enemy shark prefab.");
            }
        }
        else
        {
            if (enemySharkPrefab == null)
                Debug.LogError("Enemy Shark Prefab is not assigned in the EnemySpawner.");

            if (waypoints.Length == 0)
                Debug.LogError("Waypoints are not assigned in the EnemySpawner.");
        }
    }
}
