using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementFP : MonoBehaviour
{
    private CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public LayerMask groundMask;
    public float jumpHeight = 3f;
    
    private Vector3 velocity;
    private bool isGrounded;
    
    private Animator _animator;
    private static readonly int IsRunning = Animator.StringToHash("isRunning");

    void Start()
    {
        controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

    }

    
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.8f, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Vector3 move = transform.right * x + transform.forward * z;
        if (move.x != 0 || move.y != 0)
        {
            _animator.SetBool(IsRunning, true);
        }
        else
        {
            _animator.SetBool(IsRunning, false);
        }
        controller.Move(Time.deltaTime * speed * move);

        if (Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = (float)Math.Sqrt(jumpHeight * -2 * gravity);
        }
        
        if (Input.GetKey(KeyCode.R))
        {
            velocity.y = 30;
            Debug.Log("CHEAT JUMP used.");
        }
        
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
