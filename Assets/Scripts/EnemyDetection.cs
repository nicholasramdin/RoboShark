using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public float detectionRange = 10f;
    public float fieldOfViewAngle = 45f; // This is half of the actual FOV.
    public LayerMask detectionLayer;
    public float chaseSpeed = 5f;
    public Material sharkMaterial; // Assign this in the Inspector.

    private Transform target;
    private bool isChasing;
    private Color originalColor; // To store the original color

    void Start()
    {
        // Cache the original color of the shark's material.
        if (sharkMaterial != null)
        {
            originalColor = sharkMaterial.color;
        }
        else
        {
            Debug.LogError("Shark material has not been assigned in the inspector.", this);
        }
    }

    void Update()
    {
        if (isChasing && target != null)
        {
            ChaseTarget();
        }
        else
        {
            ScanForPlayer();
        }
    }

    void ScanForPlayer()
    {
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

    }

    void OnTargetDetected(Transform detectedTarget)
    {
        Debug.Log(detectedTarget.name + " detected!");
        target = detectedTarget;
        isChasing = true;
        // Change color to red if the material is assigned
        if (sharkMaterial != null)
        {
            sharkMaterial.color = Color.red;
        }
    }

    void ChaseTarget()
    {
        // Chase the target
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * chaseSpeed * Time.deltaTime;

        // Look at the target
        transform.LookAt(target);

        // Check if the player is still in the field of view
        if (Vector3.Angle(transform.forward, target.position - transform.position) > fieldOfViewAngle)
        {
            // Player escaped
            isChasing = false;
            target = null;
            // Reset color to the original color if the material is assigned
            if (sharkMaterial != null)
            {
                sharkMaterial.color = originalColor;
            }
        }
    }
}
