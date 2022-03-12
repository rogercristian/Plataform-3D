using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float ropeExitTime;
    public float distanceFoot;
    public float attackJumForce;
    public int damageOnJump;


    PlayerMovement m_Movement;
    PlayerController controller;
    PlayerRopeSwingging ropeSwingging;
    PlayerLife life;
    PlayerAttack attack;

    float currentRopeExitTime;
    bool isJumpingRope = false; 

    private void Start()
    {
        m_Movement = GetComponent<PlayerMovement>();
        controller = GetComponent<PlayerController>();
        ropeSwingging = GetComponent<PlayerRopeSwingging>();
        life = GetComponent<PlayerLife>();
        attack = GetComponent<PlayerAttack>();  
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
