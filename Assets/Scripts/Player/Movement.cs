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
        
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        isCeiling = Physics.CheckSphere(groundCheck.position, groundDistance, ceilingMask);
        gravityPad = Physics.CheckSphere(groundCheck.position, groundDistance, gravityPadMask);
        jumpPad = Physics.CheckSphere(groundCheck.position, groundDistance, jumpPadMask);

        bool gravityChange = gravityPad;
        Debug.Log(gravity);
        
        if (isGrounded && velocity.y < 0 || isCeiling && velocity.y < -3f)
        {
            velocity.y = -2f;
        }

        if (isCeiling && velocity.y > 0 || isGrounded && velocity.y > 0f)
        { 
            velocity.y = 2f;
        }

        if (velocity.y > 20f)
        {
            velocity.y = 20f;
        }

        if (jumpPad)
        {
            velocity.y = 0;
            velocity.y += 13;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        
        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        
        
        if (Input.GetButtonDown("Jump") && isCeiling ||Input.GetButtonDown("Jump") && isGrounded && gravity > 0)
        {
            velocity.y = jumpHeight * gravity * -0.5f;
        }

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
        
        if (dash)
        {
            speed = 11;
        }
        else if (!dash)
        {
            speed = 8;
        }
    }
}
