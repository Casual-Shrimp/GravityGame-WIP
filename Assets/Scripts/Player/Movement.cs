using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Vector3 = UnityEngine.Vector3;

public class Movement : MonoBehaviour
{

    public CharacterController controller;

    public float speed;
    public float gravity = -19.62f;
    public float jumpHeight = 1f;


    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;
    public LayerMask ceilingMask;
    public LayerMask gravityPadMask;
    public LayerMask jumpPadMask;
    
    //fov stuff
    public float startFOV = 72f;
    public float currentFOV;
    public float playerSetFOV = 72f;
    private Camera _firstPerson;

    public Vector3 velocity;
    public bool isGrounded;
    public bool isCeiling;
    public bool gravityPad;
    public bool jumpPad;


    private void Start()
    {
        speed = 10;
        currentFOV = startFOV;
        _firstPerson = Camera.main;
    }

    void Update()
    {
        //creates little invisible sphere beneath the player to check for those parameters
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        isCeiling = Physics.CheckSphere(groundCheck.position, groundDistance, ceilingMask);
        gravityPad = Physics.CheckSphere(groundCheck.position, groundDistance, gravityPadMask);
        jumpPad = Physics.CheckSphere(groundCheck.position, groundDistance, jumpPadMask);

        //this means, that if the player touches the "gravity pad" the CURRENT gravity will be inverted meaning that if you are upside down
        //and touch it again you will be set to "normal gravity"
        bool gravityChange = gravityPad;

        //inverts the gravity when bool is set to true
        if (gravityChange)
        {
            gravity = gravity * -1;
            velocity.y = velocity.y * -1;
            //need to rework this thing to smoothly turn 180 degrees
            transform.Rotate(0, 0, 180);
        }

    }

    private void FixedUpdate()
    {
        ThisBoiMoving();
        ThisBoiSpeedy();
        velocity.y += gravity * Time.deltaTime;

        
        //calls everything that is supposed to happen when gravity is normal meaning < 0
        if (gravity < 0)
        {
            NormalGravity();
        }
        
        //calls everything that is supposed to happen when gravity is reversed meaning > 0  
        if (gravity > 0)
        {
            ReverseGravity();
        }
    }

    void ThisBoiMoving()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * (speed * Time.deltaTime));
        controller.Move(velocity * Time.deltaTime);
    }

    void ThisBoiSpeedy()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed *= 1.01f;
            if (speed >= 15f)
            {
                speed = 15f;
            }

            currentFOV += 20f;

        }
        else
        {
            speed /= 1.02f;
            if (speed <= 10)
            { 
                speed = 10f;
            }

            currentFOV = playerSetFOV;

        }
    }
    
    void NormalGravity()
    {
        //resets velocity to -2 when any ground surface is touched
        if (isGrounded || isCeiling)
        {
            velocity.y = -2f;
        }
        
        if (Input.GetButton("Jump") && isGrounded || Input.GetButton("Jump") && isCeiling)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        
        //caps the maximum velocity when falling
        if (velocity.y > 100f)
        {
            velocity.y = 100f;
        }
        
        //as the name states, pushes you high in the air when the player touches the layer "JumpPad"
        //when the player touches the layer, it turns the bool "jumpPad" true for enough time to execute the code beneath
        if (jumpPad)
        {
            velocity.y = 0;
            velocity.y += 13;
        }
        
    }

    void ReverseGravity()
    {
        if (isGrounded || isCeiling)
        {
            velocity.y = 2f;
        }

        if (Input.GetButton("Jump") && isCeiling || Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = jumpHeight * gravity * -0.5f;
        }
        
        //pushes you in the air
        if (jumpPad)
        {
            velocity.y = 0;
            velocity.y -= 13;
        }
    }
}

 