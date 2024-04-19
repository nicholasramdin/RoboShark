using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolRoute : MonoBehaviour
{
    public Transform[] waypoints;
    public float patrolSpeed = 2.0f;
    private int currentWaypointIndex = 0;
    private bool movingForward = true;

    private void Update()
    {
        PatrolBackAndForth();
    }

    private void PatrolBackAndForth()
    {
        // If we have no waypoints, or only one, return early.
        if (waypoints.Length <= 1) return;

        // Move the enemy towards the current waypoint.
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        float step = patrolSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, step);

        // Check if the enemy has reached the waypoint.
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            if (movingForward)
            {
                // Move to the next waypoint.
                currentWaypointIndex++;
                // If we've reached the last waypoint, reverse the direction.
                if (currentWaypointIndex >= waypoints.Length)
                {
                    currentWaypointIndex = waypoints.Length - 2;
                    movingForward = false;
                }
            }
            else
            {
                // Move to the previous waypoint.
                currentWaypointIndex--;
                // If we've reached the first waypoint, reverse the direction.
                if (currentWaypointIndex < 0)
                {
                    currentWaypointIndex = 1;
                    movingForward = true;
                }
            }
        }

        // Rotate to face the next waypoint.
        if (step > 0)
        {
            var rotation = Quaternion.LookRotation(targetWaypoint.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * patrolSpeed);
        }
    }
}

