using UnityEngine;

public class RobotSharkController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    private Rigidbody rb;
    private bool facingRight = true;
    public int maxEggs = 5; // The maximum number of eggs the player can carry
    private int currentEggs = 0; // The current number of eggs the player is carrying
    public int score = 0; // The player's current score

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
            rb.MoveRotation(Quaternion.Euler(0, 270, 0));
            facingRight = true;
        }
        else if (horizontal < 0 && facingRight)
        {
            rb.MoveRotation(Quaternion.Euler(0, 90, 0));
            facingRight = false;
        }
    }

    public bool CollectEggs(int amount)
    {
        if (currentEggs + amount <= maxEggs)
        {
            currentEggs += amount;
            Debug.Log("Eggs collected. Current eggs: " + currentEggs);
            return true; // Eggs were successfully collected
        }
        else
        {
            Debug.Log("Cannot collect more eggs. Max capacity reached.");
            return false;
        }
    }

    public void DepositEggs()
    {
        if (currentEggs > 0)
        {
            score += currentEggs; // Add to the score based on the number of eggs
            currentEggs = 0; // Reset the count of carried eggs
            Debug.Log("Eggs deposited. Score: " + score);
        }
    }

    public int GetCurrentEggs()
    {
        return currentEggs;
    }
}
