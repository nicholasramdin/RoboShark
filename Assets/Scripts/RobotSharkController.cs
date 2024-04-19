using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RobotSharkController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float turnSpeed = 60.0f;

    void Update()
    {
        // Get the main camera's transform
        Transform camTransform = Camera.main.transform;

        // Get input for movement relative to the camera's orientation
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float depth = Input.GetAxis("Depth");

        // Calculate the movement direction in the camera's local space
        Vector3 camRight = camTransform.right; // Camera's right vector
        Vector3 camForward = camTransform.forward; // Camera's forward vector
        camForward.y = 0; // Ignore camera's vertical component to keep movement horizontal
        camRight.y = 0; // Ignore camera's vertical component for horizontal movement

        // Normalize vectors to ensure their length is 1
        camForward.Normalize();
        camRight.Normalize();

        // Calculate the desired movement direction in world space
        Vector3 direction = (camRight * horizontal + camForward * vertical + Vector3.up * depth).normalized;

        // Translate the shark in the desired direction
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);

        //  rotate the shark to face the direction of movement
        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
        }
    }
}


