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
    
    public float speed = 8;
    public float gravity = 9.81f;
    public float jumpHeight = 1.5f;
    public float dashTime;
    public float dashCooldown;
    

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
        
        //those if statements reset the velocity brought by the physics with the player controller to not always accelerate
        if (isGrounded && velocity.y < 0 || isCeiling && velocity.y < -3f)
        {
            velocity.y = -2f;
        }

        if (isCeiling && velocity.y > 0 || isGrounded && velocity.y > 0f)
        { 
            velocity.y = 2f;
        }
        
        //caps the maximum velocity when falling 
        if (velocity.y > 20f)
        {
            velocity.y = 20f;
        }
        
        //as the name states, pushes you high in the air when the player touches the layer "JumpPad"
        //when the player touches the layer, it turns the bool "jumpPad" true for enough time to execute the code beneath
        if (jumpPad)
        {
            velocity.y = 0;
            velocity.y += 13;
        }
        
        //checks if gravity is inverted to let you use the jump pad on the ceiling
        if (jumpPad && gravity > 0)
        {
            velocity.y = 0;
            velocity.y -= 13;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        
        //lets you jump on the ground
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        
        //lets you jup when with inverted gravity on any (yet coded) layer
        if (Input.GetButtonDown("Jump") && isCeiling ||Input.GetButtonDown("Jump") && isGrounded && gravity > 0)
        {
            velocity.y = jumpHeight * gravity * -0.5f;
        }
    
        //inverts the gravity when bool is set to true
        if (gravityChange)
        {
            gravity = gravity * -1;
            velocity.y = velocity.y * -1;
            transform.Rotate(0, 0, 180);
        }
        
        
        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        Dash();

    }
    
    void Dash()
    {
        
        bool dash = Input.GetKey(KeyCode.LeftShift);

        if (dash && dashCooldown > Time.time)
        {
            speed = 11;
            dashTime += 1;
        }
        else if (!dash)
        {
            speed = 8;
        }
        
        

    }
}
