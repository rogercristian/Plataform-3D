using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerCollision
{
    public float right;
    public float left;
    public float ground;
}

[System.Serializable]
public class Swinning
{
    public float gravity;
    public float moveForce;
    public float jumpForce;
    public float maxSpeed;
    public float maxJumpSpeed;
}
[System.Serializable]
public class Walking
{
    public float moveForce;
    public float maxSpeed;
    public float jumpForce;
    public float wallJumpForce;
    public float maxJumpSpeed;
    public int totalJump = 2;
}
public class PlayerMovement : MonoBehaviour
{
    public PlayerCollision collision;
    public Swinning swinning;
    public Walking walking;
    

    public Transform model;

    private Rigidbody m_Rigidbody;    
    private PlayerAttack attack;
    private bool m_IsGrounded = false;
    private bool m_IsCollisionLefft;
    private bool m_IsCollisionRight;
    private bool m_IsSwinning = false;
    private int m_Jumps = 0;

    private GameObject m_LastWallTrouched;
    private GameObject m_WallTouching;
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();    
        attack = GetComponent<PlayerAttack>();

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        m_IsCollisionRight = Physics.Raycast(new Ray(transform.position, Vector3.right), out hit, collision.right);

        if(m_IsCollisionRight && hit.collider.gameObject != null)
        {
            m_LastWallTrouched = hit.collider.gameObject;
        }
           
        m_IsCollisionLefft = Physics.Raycast(new Ray(transform.position, Vector3.left), out hit, collision.left);
        if (m_IsCollisionLefft && hit.collider.gameObject != null)
        {
            m_LastWallTrouched = hit.collider.gameObject;
        }

        bool wasGrounded = m_IsGrounded;
        m_IsGrounded = Physics.Raycast(new Ray(transform.position, Vector3.down), out hit, collision.ground);
        if (!wasGrounded && m_IsGrounded)
        {
            m_Jumps = 0;
            m_LastWallTrouched = null;
            m_WallTouching = null;
        }

    }
    public void Jump()
    {
        //if (!m_IsGrounded && m_Jumps == 0) return;
        if (!m_IsGrounded && (m_IsCollisionRight || m_IsCollisionLefft))
            {
                if(m_WallTouching != m_LastWallTrouched) {
                    if (m_IsCollisionRight)
                    {
                        JumpLeft();
                    }
                    else
                    {
                        JumpRight();
                    }
                m_Jumps = walking.totalJump;
                ClampJump();
                m_WallTouching = m_LastWallTrouched;
            }
        }
        else if (m_Jumps < walking.totalJump)
        {
            if ((m_Jumps == 0 && m_IsGrounded) || m_Jumps > 0)
            {
                JumpUp();
            
             }
            ClampJump();
        }
    }

    public void Jump(float jumpForce)
    {
        Vector3 v = m_Rigidbody.velocity;
        v.y = 0;
        m_Rigidbody.velocity = v;

        JumpUp(jumpForce);
    }

    private void JumpLeft()
    {       
        m_Rigidbody.AddForce(-walking.wallJumpForce, walking.wallJumpForce, 0f);       
        m_Jumps++;
    }
    private void JumpRight()
    {
        m_Rigidbody.AddForce(walking.wallJumpForce, walking.wallJumpForce, 0f);        
        m_Jumps++;
    }  

    private void JumpUp()
    { 
        float jumpForce = m_IsSwinning? swinning.jumpForce: walking.jumpForce;
        JumpUp(jumpForce);
    }

    private void JumpUp(float jumpForce)
    {
        m_Rigidbody.AddForce(0f, jumpForce, 0f);
   
        m_Jumps++;
    }
    private void ClampJump()
    {
        float maxJumpSpeed = m_IsSwinning ? swinning.maxJumpSpeed : walking.maxJumpSpeed;
        Vector3 maxVelocity = m_Rigidbody.velocity;
        maxVelocity.y = Mathf.Clamp(maxVelocity.y, -maxJumpSpeed, maxJumpSpeed);
        m_Rigidbody.velocity = maxVelocity;
    }
    public void Move(Vector2 input)
    {
        float maxSpeed, moveForce;

        if (m_IsSwinning)
        {
            moveForce = swinning.moveForce;
            maxSpeed = swinning.maxSpeed;
        }
        else
        {
            input.y = 0;
            moveForce = walking.moveForce;
            maxSpeed = walking.maxSpeed;            
        }

        m_Rigidbody.AddForce(new Vector3(input.x, input.y, 0f) * moveForce * Time.deltaTime);
        Vector3 maxVelocity = m_Rigidbody.velocity;

        if (!attack.IsAttacking())
        {
            maxVelocity.x = Mathf.Clamp(maxVelocity.x, -maxSpeed * -input.x, maxSpeed * input.x);
        }

        m_Rigidbody.velocity = maxVelocity;

        WhatLookingDirection(input.x);
    }
    private void WhatLookingDirection(float direction)
    {
        Quaternion rotate = model.localRotation;
        if (direction > 0)
        {
            rotate.y = 0f;           
        }
        else if(direction < 0)
        {
            rotate.y = 180;           
        }

        model.localRotation = rotate;       
    }
    private void StartSwin()
    {
        m_Rigidbody.useGravity = false;
        m_Rigidbody.velocity = new Vector3(0f, swinning.gravity, 0f);
        m_IsSwinning = true;
    }
    private void StopSwin()
    {
        m_Rigidbody.useGravity = true;
        m_IsSwinning = false;
        m_Rigidbody.AddForce(0f, swinning.jumpForce, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {           

        if (other.CompareTag("Water"))
        {
            StartSwin();
        }
    }
    private void OnTriggerStay(Collider other)
    {
       
        
        
    }
    private void OnTriggerExit(Collider other)
    {
       
        
        if (other.CompareTag("Water"))
        {
            StopSwin();
        }
    }
}
