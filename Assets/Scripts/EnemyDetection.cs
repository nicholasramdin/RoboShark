using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public float detectionRange = 10f;
    public float fieldOfViewAngle = 45f;
    public LayerMask detectionLayer;
    public float chaseSpeed = 40f;
    public Material normalMaterial;
    public Material alertMaterial;

    private Transform target;
    private bool isChasing;
    private Renderer sharkRenderer;
    private PatrolRoute patrolRoute;

    void Start()
    {
        sharkRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        patrolRoute = GetComponent<PatrolRoute>();
        sharkRenderer.material = normalMaterial;
    }

    void Update()
    {
        if (isChasing && target != null)
        {
            ChaseTarget();
            if (Vector3.Distance(transform.position, target.position) < 1f) // Close enough to catch the player
            {
                GameManager.Instance.HandleGameOver();
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
        foreach (Collider targetCollider in targetsInViewRadius)
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
        sharkRenderer.material = alertMaterial;
        patrolRoute.enabled = false; // Stop patrolling when chasing
    }

    void ChaseTarget()
    {
        transform.position += (target.position - transform.position).normalized * chaseSpeed * Time.deltaTime;
        transform.LookAt(target.position);
    }

    void LoseTarget()
    {
        isChasing = false;
        target = null;
        sharkRenderer.material = normalMaterial;
        patrolRoute.enabled = true; // Resume patrol
    }
}
