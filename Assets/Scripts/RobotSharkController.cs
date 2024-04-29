using UnityEngine;

public class RobotSharkController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    private Rigidbody rb;
    private bool facingRight = true;
    public int maxEggs = 5; // Maximum number of eggs the player can hold
    private int currentEggs = 0; // Current number of eggs the player is holding
    public int score = 0; // Player's score

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
        rb.useGravity = false;
    }

    void Update()
    {
        // Capture the directions relative to the camera's orientation
        Transform camTransform = Camera.main.transform;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate the movement direction relative to camera's orientation
        Vector3 camRight = camTransform.right;
        Vector3 camForward = camTransform.up; // Using up vector to move in the camera's up/down direction

        // Remove any influence from the camera's vertical (z-axis) tilt
        camRight.z = 0;
        camForward.z = 0;
        camRight.Normalize();
        camForward.Normalize();

        // Calculate the desired movement direction in world space
        Vector3 direction = (camRight * horizontal + camForward * vertical).normalized;

        // Translate the shark in world space, using camera's right and up for horizontal and vertical movement
        rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);

        // Flipping the shark's sprite based on the movement direction
        if (horizontal > 0 && !facingRight)
        {
            // Rotate the shark to face right using physics
            Quaternion newRotation = Quaternion.Euler(0, 270, 0);
            rb.MoveRotation(newRotation);
            facingRight = true;
        }
        else if (horizontal < 0 && facingRight)
        {
            // Rotate the shark to face left (180 degrees) using physics
            Quaternion newRotation = Quaternion.Euler(0, 90, 0);
            rb.MoveRotation(newRotation);
            facingRight = false;
        }
    }
    public void CollectEggs(int value)
    {
        if (currentEggs < maxEggs)
        {
            currentEggs += value;
            if (currentEggs > maxEggs)
            {
                currentEggs = maxEggs;
            }
            Debug.Log("Eggs collected: " + currentEggs);
        }
    }

    // Call this method when the player deposits eggs
    public void DepositEggs()
    {
        if (currentEggs > 0)
        {
            score += currentEggs; // Add to the score
            currentEggs = 0; // Reset the egg count
            Debug.Log("Score: " + score);
        }
    }
}
