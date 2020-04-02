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
    public Transform cellingCheck;
    public LayerMask groundMask;
    public float jumpHeight = 2f;
    public Camera cameraView;
    
    private Vector3 velocity;
    private bool isGrounded;
    private bool isCellingAbove;
    private bool crouchedRecently = false;
    
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
        isCellingAbove = Physics.CheckSphere(cellingCheck.position, 0.8f, groundMask);

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

        if (Input.GetButtonDown("Jump") && isGrounded && !isCellingAbove)
        {
            velocity.y = (float)Math.Sqrt(jumpHeight * -2 * gravity);
        }
        
        if (Input.GetKey(KeyCode.R))
        {
            velocity.y = 30;
            Debug.Log("CHEAT JUMP used.");
        }
        
        //SPRINT
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed += 5;
            Debug.Log("Sprint activated.");
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed -= 5;
            Debug.Log("Sprint deactivated.");
        }
        
        //CROUCH
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            controller.height = 1;
            controller.center = new Vector3(0, 0.5f, 0);
            cameraView.transform.localPosition = new Vector3(0, 1f, 0);
            crouchedRecently = true;
        }
        if (!Input.GetKey(KeyCode.LeftControl) && !isCellingAbove && crouchedRecently)
        {
            controller.height = 1.75f;
            controller.center = new Vector3(0, 0.9f, 0);
            cameraView.transform.localPosition = new Vector3(0, 1.75f, 0);
            crouchedRecently = false;
            Debug.Log($"{Time.deltaTime}");
        }
        
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
