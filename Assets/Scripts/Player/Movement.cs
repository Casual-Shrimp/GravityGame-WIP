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
    public float groundDistance = 0.4f;
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

    [Header("Rotation")]
    public bool shouldRotate = false;
    private float rotationSpeed = 1.8f;
    
    private Quaternion startRotation;
    private Quaternion targetRotation;
    private float rotationProgress = 0f;

    private Camera mainCam;
    private float baseFOV;


    private void Start()
    {
        speed = 10;
        startRotation = transform.rotation; // Start with the current rotation

        // Set targetRotation to rotate 180 degrees on the z-axis, flipping the player upside down
        targetRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + 180);
        mainCam = Camera.main;
        baseFOV = mainCam.fieldOfView;
    }

    void Update()
    {
        //creates little invisible sphere beneath the player to check for those parameters
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        isCeiling = Physics.CheckSphere(groundCheck.position, groundDistance, ceilingMask);
        gravityPad = Physics.CheckSphere(groundCheck.position, groundDistance, gravityPadMask);
        jumpPad = Physics.CheckSphere(groundCheck.position, groundDistance, jumpPadMask);
        currentTime = Time.time;
       
    }

    private void FixedUpdate()
    {
        ThisBoiMoving();
        ThisBoiSpeedy();
        Crouch();
        velocity.y += gravity * Time.deltaTime;

        bool gravityChange = gravityPad;
        
        if (shouldRotate)
        {
            rotationProgress += rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, rotationProgress);

            if (rotationProgress >= 1f)
            {
                shouldRotate = false;
                rotationProgress = 0f;
            }
        }
        
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

        // Set startRotation to the current rotation
        startRotation = transform.rotation;

        // Set targetRotation to flip 180 degrees on the local z-axis
        targetRotation = startRotation * Quaternion.Euler(0, 0, 180);
    }

    void ThisBoiMoving()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * (speed * Time.deltaTime));
        controller.Move(velocity * Time.deltaTime);
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
            velocity.y += 20;
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
            velocity.y -= 20;
        }
    }

    void ThisBoiSpeedy()
    {
        bool pressedShift = Input.GetKey(KeyCode.LeftShift);
        bool pressedDirection = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

        if (pressedShift && pressedDirection)
        {
            speed *= 1.03f;
            if (speed >= 15f)
            {
                speed = 15f;
            }

            {
                mainCam.fieldOfView *= 1.03f;
            }
            if (mainCam.fieldOfView >= 90)
            {
                mainCam.fieldOfView = 90;
            }
        }
        else
        {
            speed /= 1.05f;
            if (speed <= 10)
            {
                speed = 10f;
            }

            mainCam.fieldOfView /= 1.05f;
            if (mainCam.fieldOfView <= baseFOV)
            {
                mainCam.fieldOfView = baseFOV;
            }
        }
    }
    
    

    void Crouch()
    {
    

    }
}

 