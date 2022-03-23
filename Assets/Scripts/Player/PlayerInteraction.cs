using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float ropeExitTime;
    public float distanceFoot;
    public float distanceHead;
    public float attackJumForce;
    public int damageOnJump;


    PlayerMovement m_Movement;
    PlayerController controller;
    PlayerRopeSwingging ropeSwingging;
    PlayerLife life;
    PlayerAttack attack;
    Rigidbody rb;

    float currentRopeExitTime;
    bool isJumpingRope = false; 

    private void Start()
    {
        m_Movement = GetComponent<PlayerMovement>();
        controller = GetComponent<PlayerController>();
        ropeSwingging = GetComponent<PlayerRopeSwingging>();
        life = GetComponent<PlayerLife>();
        attack = GetComponent<PlayerAttack>();  
        rb = GetComponent<Rigidbody>();
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

        RaycastHit hit;

        if (Physics.Raycast(new Ray(transform.position, Vector3.down), out hit, distanceFoot))
        {
            DestructbleBeahavior destructble = hit.collider.GetComponent<DestructbleBeahavior>();
            if (destructble != null)
            {
                destructble.ApplyDamage(damageOnJump);
                m_Movement.Jump(attackJumForce);
            }
        }

        if(Physics.Raycast(new Ray(transform.position, Vector3.up), out hit, distanceHead))
        {
            DestructbleBeahavior destructble = hit.collider.GetComponent<DestructbleBeahavior>();
            if(destructble != null && destructble.GetComponent<EnemyBehavior>() == null)
            {
                destructble.ApplyDamage(damageOnJump);
                Vector3 v = rb.velocity;
                v.y = 0;
                rb.velocity = v;
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
    }


    private void OnTriggerExit(Collider other)
    {
      
    }

    private void OnTriggerStay(Collider other)
    {
        Trampolin trampolin = other.GetComponent<Trampolin>();

        if (trampolin != null)
        {
            m_Movement.Jump(trampolin.jumpForce);
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

        EnemyBehavior enemy = collision.gameObject.GetComponentInParent<EnemyBehavior>();
        if (enemy != null)
        {
            if (attack.IsAttacking())
            {
                enemy.ApplyDamage(attack.damage);
            }

            life.ApplyDamage();

        }
    }

    public void SetIsJumpingRope()
    {
        isJumpingRope = true;
        
    }
}
