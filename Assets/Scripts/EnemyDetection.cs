using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public float detectionRange = 10f;
    public float fieldOfViewAngle = 45f; // This is half of the actual FOV.
    public LayerMask detectionLayer;
    public float chaseSpeed = 40f;
    public Material normalMaterial; // Assign the normal shark material in the Inspector.
    public Material alertMaterial; // Assign the alert (red) material in the Inspector.
    public float catchDistance = 1.5f; // Distance at which the player is caught

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
            Debug.LogError("PatrolRoute script is not attached to " + gameObject.name, this);
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
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, detectionRange, detectionLayer);
        foreach (var targetCollider in targetsInViewRadius)
        {
            Transform potentialTarget = targetCollider.transform;
            Vector3 directionToTarget = (potentialTarget.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < fieldOfViewAngle)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, directionToTarget, out hit, detectionRange, detectionLayer))
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
        target = detectedTarget;
        isChasing = true;
        if (sharkRenderer != null && alertMaterial != null)
        {
            sharkRenderer.material = alertMaterial;
        }
        if (patrolRoute != null) patrolRoute.enabled = false;
    }

    void ChaseTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * chaseSpeed * Time.deltaTime;
        transform.LookAt(target);

        if (Vector3.Distance(transform.position, target.position) <= catchDistance)
        {
            CatchPlayer();
        }

        if (Vector3.Angle(transform.forward, target.position - transform.position) > fieldOfViewAngle || Vector3.Distance(transform.position, target.position) > detectionRange)
        {
            LoseTarget();
        }
    }

    void CatchPlayer()
    {
        GameManager.Instance.HandleGameOver(); // Handle game over when the shark catches the player
    }

    void LoseTarget()
    {
        isChasing = false;
        target = null;
        if (sharkRenderer != null && normalMaterial != null)
        {
            sharkRenderer.material = normalMaterial;
        }
        if (patrolRoute != null) patrolRoute.enabled = true;
    }
}
