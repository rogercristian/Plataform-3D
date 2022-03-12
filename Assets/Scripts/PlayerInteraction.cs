using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float ropeExitTime;

    PlayerMovement m_Movement;
    PlayerController controller;
    PlayerRopeSwingging ropeSwingging;


    float currentRopeExitTime;
    bool isJumpingRope = false; 

    private void Start()
    {
        m_Movement = GetComponent<PlayerMovement>();
        controller = GetComponent<PlayerController>();
        ropeSwingging = GetComponent<PlayerRopeSwingging>();
    }

    private void Update()
    {
        
        if (isJumpingRope)
        {
            
            currentRopeExitTime += Time.deltaTime;
            Physics.IgnoreLayerCollision(0, 7, true);
            if (currentRopeExitTime > ropeExitTime)
            {              
                currentRopeExitTime = 0;
                isJumpingRope = false;
                Physics.IgnoreLayerCollision(0, 7, false);
            }
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            GameController.AddCoin(1);
            GameObject.Destroy(other.gameObject);
        }

        Trampolin trampolin = other.GetComponent<Trampolin>();

        if (trampolin != null)
        {
            m_Movement.Jump(trampolin.jumpForce);
        }
    }


    private void OnTriggerExit(Collider other)
    {
       
        if (other.transform.CompareTag("Water"))
        {
          
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Water"))
        {
           
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rope") && !isJumpingRope)
        {
            controller.SetIsRopeSwingging(true);          

            RopeBehavior rope = collision.gameObject.GetComponentInParent<RopeBehavior>();

            if (rope != null)
            {
                ropeSwingging.AttachRope(rope, collision.gameObject);
            }
        }
    }

    public void SetIsJumpingRope()
    {
        isJumpingRope = true;
        
    }
}
