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
        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogError("Waypoints not assigned on " + gameObject.name);
            return;
        }

        PatrolBackAndForth();
    }

    private void PatrolBackAndForth()
    {
        if (waypoints.Length <= 1) return;  // Early exit if not enough waypoints

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        float step = patrolSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, step);

        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            if (movingForward)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex >= waypoints.Length)
                {
                    currentWaypointIndex = waypoints.Length - 2;
                    movingForward = false;
                }
            }
            else
            {
                currentWaypointIndex--;
                if (currentWaypointIndex < 0)
                {
                    currentWaypointIndex = 1;
                    movingForward = true;
                }
            }
        }

        var rotation = Quaternion.LookRotation(targetWaypoint.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * patrolSpeed);
    }
}
