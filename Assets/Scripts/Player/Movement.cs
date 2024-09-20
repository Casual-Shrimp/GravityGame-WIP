using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;

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
    

    public Vector3 velocity;
    public bool isGrounded;
    public bool isCeiling;
    public bool gravityPad;
    public bool jumpPad;

    private float gravityChangeTime = 1.2f;
    private float currentTime;
    private float passedTime = 0;
    
    //rotation
    public bool shouldRotate = false;
    private float rotationSpeed = 1.5f;
    
    private Quaternion startRotation;
    private Quaternion targetRotation;
    private float rotationProgress = 0f;

    private void Start()
    {
        speed = 10;
        startRotation = Quaternion.Euler(0, transform.rotation.y, transform.rotation.z);
        targetRotation = Quaternion.Euler(180, transform.rotation.y, transform.rotation.z );
    }

    void Update()
    {
        //creates little invisible sphere beneath the player to check for those parameters
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        isCeiling = Physics.CheckSphere(groundCheck.position, groundDistance, ceilingMask);
        gravityPad = Physics.CheckSphere(groundCheck.position, groundDistance, gravityPadMask);
        jumpPad = Physics.CheckSphere(groundCheck.position, groundDistance, jumpPadMask);
        currentTime = Time.time;
        if (shouldRotate)
        {
            rotationProgress += rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, rotationProgress);
            
            if (rotationProgress >= 1f)
            {
                shouldRotate = false;
                rotationProgress = 0f;
                
                // Swap start and target rotations for the next rotation
                Quaternion temp = startRotation;
                startRotation = targetRotation;
                targetRotation = temp;
            }
        }
    }

    private void FixedUpdate()
    {
        ThisBoiMoving();
        ThisBoiSpeedy();
        velocity.y += gravity * Time.deltaTime;

        bool gravityChange = gravityPad;
        
        
        
        //inverts the gravity when bool is set to true
        if (gravityChange && passedTime < currentTime)
        {
            gravity = gravity * -1;
            velocity.y = velocity.y * -1;
            //need to rework this thing to smoothly turn 180 degrees
            ToggleRotation();
            //transform.Rotate(new Vector3(transform.rotation.x, transform.rotation.y));
            passedTime = currentTime + gravityChangeTime;
        }

        
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
     public void ToggleRotation()
    {
        shouldRotate = true;
        rotationProgress = 0f;
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
            

        }
        else
        {
            speed /= 1.02f;
            if (speed <= 10)
            { 
                speed = 10f;
            }
            

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
            velocity.y += 9;
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
            velocity.y -= 9;
        }
    }
}

 