using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public float detectionRange = 10f;
    public float fieldOfViewAngle = 45f; // This is half of the actual FOV. 45 means a 90-degree FOV.
    public LayerMask detectionLayer; // Make sure this is set in the Inspector.

    void Update()
    {
        ScanForPlayer();
    }

    void ScanForPlayer()
    {
        // Find all targets within the detection range and in the specified layer.
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, detectionRange, detectionLayer);

        foreach (var target in targetsInViewRadius)
        {
            Transform targetTransform = target.transform;
            Vector3 directionToTarget = (targetTransform.position - transform.position).normalized;

            // Check if the target is within the field of view
            if (Vector3.Angle(transform.forward, directionToTarget) < fieldOfViewAngle)
            {
                float distanceToTarget = Vector3.Distance(transform.position, targetTransform.position);

                // Raycast to check for line of sight
                RaycastHit hit;
                if (Physics.Raycast(transform.position, directionToTarget, out hit, distanceToTarget, detectionLayer))
                {
                    if (hit.transform == targetTransform)
                    {
                        // Player detected
                        OnTargetDetected(targetTransform);
                    }
                }
            }
        }
    }

    void OnTargetDetected(Transform target)
    {
        // Add logic for when the target is detected, like initiating chase
        Debug.Log(target.name + " detected!");
        // Implement the response, such as starting chase behavior.
    }
}
