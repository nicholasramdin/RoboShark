using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolRoute : MonoBehaviour
{
    public Transform[] waypoints;
    public float patrolSpeed = 2.0f;
    private int currentWaypointIndex = 0;

    private void Update()
    {
        PatrolBetweenWaypoints();
    }

    private void PatrolBetweenWaypoints()
    {
        // If no waypoints, return early.
        if (waypoints.Length == 0) return;

        // Move the enemy towards the current waypoint.
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        float step = patrolSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, step);

        // Check if the enemy has reached the waypoint.
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            // Increment the currentWaypointIndex, wrapping back to 0 if necessary.
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
}

