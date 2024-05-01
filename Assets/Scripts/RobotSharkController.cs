using UnityEngine;
using TMPro; // Include this for using TextMeshPro elements

public class RobotSharkController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    private Rigidbody rb;
    private bool facingRight = true;

    public int maxEggs = 5; // The maximum number of eggs the player can carry
    private int currentEggs = 0; // The current number of eggs the player is carrying
    public int score = 0; // The player's current score

    // UI elements
    public TextMeshProUGUI scoreText; // Assign in inspector
    public TextMeshProUGUI eggCountText; // Assign in inspector
    public TextMeshProUGUI promptText; // Assign in inspector

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
        rb.useGravity = false;
        UpdateUI(); // Initial UI update to set the texts properly
    }

    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Transform camTransform = Camera.main.transform;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 camRight = camTransform.right;
        Vector3 camForward = camTransform.up;

        camRight.z = 0;
        camForward.z = 0;
        camRight.Normalize();
        camForward.Normalize();

        Vector3 direction = (camRight * horizontal + camForward * vertical).normalized;
        rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);

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
            UpdateUI();
            return true;
        }
        else
        {
            UpdateUI();
            return false;
        }
    }

    public void DepositEggs()
    {
        if (currentEggs > 0)
        {
            score += currentEggs;
            currentEggs = 0;
            UpdateUI();
        }
    }

    public int GetCurrentEggs()
    {
        return currentEggs;
    }

    private void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        eggCountText.text = "Eggs: " + currentEggs + "/" + maxEggs;

        if (currentEggs >= maxEggs)
        {
            promptText.text = "Max capacity reached. Go to deposit point.";
        }
        else
        {
            promptText.text = ""; // Clear the prompt when not needed
        }
    }
}
