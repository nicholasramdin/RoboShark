using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public float detectionRange = 10f;
    public float fieldOfViewAngle = 45f; // This is half of the actual FOV.
    public LayerMask detectionLayer;
    public float chaseSpeed = 40f;
    public Material normalMaterial; // Assign the normal shark material in the Inspector.
    public Material alertMaterial; // Assign the alert (red) material in the Inspector.

    private Transform target;
    private bool isChasing;
    private Renderer sharkRenderer; // Renderer component of the shark
    private PatrolRoute patrolRoute; // Reference to the PatrolRoute script


    void Start()
    {
        sharkRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        patrolRoute = GetComponent<PatrolRoute>(); // Reference the PatrolRoute script attached to the GameObject

        if (sharkRenderer == null)
        {
            Debug.LogError("SkinnedMeshRenderer component is not attached to " + gameObject.name, this);
        }
        else
        {
            sharkRenderer.material = normalMaterial;
        }

        if (patrolRoute == null)
        {
            Debug.LogError("EnemyPatrol script is not attached to " + gameObject.name, this);
        }
    }

    void Update()
    {
        if (isChasing && target != null)
        {
            ChaseTarget();
            // Calculate the distance to the target
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            // Check if the target has escaped
            if (distanceToTarget > detectionRange * 1.2)
            {
                LoseTarget();
            }
        }
        else
        {
            ScanForPlayer();
        }
    }

    void ScanForPlayer()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, detectionRange, detectionLayer);
        foreach (var targetCollider in targetsInViewRadius)
        {
            Transform potentialTarget = targetCollider.transform;
            Vector3 directionToTarget = (potentialTarget.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < fieldOfViewAngle)
            {
                float distanceToTarget = Vector3.Distance(transform.position, potentialTarget.position);
                RaycastHit hit;
                if (Physics.Raycast(transform.position, directionToTarget, out hit, distanceToTarget, detectionLayer))
                {
                    if (hit.transform == potentialTarget)
                    {
                        OnTargetDetected(potentialTarget);
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
        if (sharkRenderer != null && alertMaterial != null)
        {
            sharkRenderer.material = alertMaterial;
        }
        // Disable the patrol script to stop patrolling
        if (patrolRoute != null) patrolRoute.enabled = false;
    }

    void ChaseTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * chaseSpeed * Time.deltaTime;
        transform.LookAt(target);

        if (Vector3.Angle(transform.forward, target.position - transform.position) > fieldOfViewAngle)
        {
            isChasing = false;
            target = null;
            // Revert the shark's material to the normal material
            if (sharkRenderer != null && normalMaterial != null)
            {
                sharkRenderer.material = normalMaterial;
            }
        }
    }



void LoseTarget()
    {
        // Call this method when the player escapes
        Debug.Log("Player escaped, losing target.");
        isChasing = false;
        target = null;
        if (sharkRenderer != null && normalMaterial != null)
        {
            sharkRenderer.material = normalMaterial;
        }
        // Enable the patrol script to resume patrolling
        if (patrolRoute != null) patrolRoute.enabled = true;
    }
}
