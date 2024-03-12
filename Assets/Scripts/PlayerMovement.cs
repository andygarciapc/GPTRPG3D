using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust this to control the player's movement speed
    public float rotationSpeed = 10f; // Adjust this to control the player's rotation speed

    private Rigidbody rb;

    void Start()
    {
        // Get the Rigidbody component attached to the player GameObject
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

        _hasAnimator = TryGetComponent(out _animator);

        JumpAndGravity();
        GroundedCheck();
        Move();
    }
}
