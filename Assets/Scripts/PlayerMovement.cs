using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust this to control the player's movement speed
    public float gravity = 9.81f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Transform cam;

    private CharacterController controller;

    void Start()
    {
        // Get the CharacterController component attached to the player GameObject
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        // Get input from the horizontal and vertical axes (arrow keys, WASD, etc.)
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the movement direction based on input
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (movement.magnitude >= 0.1f)
        {
            //Calculate angle to face player
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Move the player using CharacterController
            Vector3 movementDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            movementDir = movementDir.normalized * moveSpeed * Time.deltaTime;
            movementDir.y -= gravity * Time.deltaTime;
            controller.Move(movementDir);
        }
    }
}
