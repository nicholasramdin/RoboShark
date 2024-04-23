using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The player's transform
    public float smoothSpeed = 0.125f; // How smoothly the camera catches up with its target
    public Vector3 offset; // The initial offset from the target

    void Start()
    {
        // Initialize the offset at start if you want to auto-detect it based on initial positions
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        // You can limit the camera's follow feature to only vertical movement by setting x (and z if needed) to the camera's current position
        desiredPosition.x = transform.position.x;
        desiredPosition.z = transform.position.z; // Keep the camera's depth fixed

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // If you want the camera to always look at the player, uncomment the following line
        //transform.LookAt(target);
    }
}
