using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    private Vector2 m_InputDirection; 
    private PlayerMovement m_Movement;
    private  PlayerRopeSwingging m_RopeSwinging;
    private PlayerLife life;
    private PlayerAttack attack;
    
    private bool isMoving;

    private bool isRopeSwingging;
    private PlayerAnimator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        m_Movement = GetComponent<PlayerMovement>();
        m_RopeSwinging = GetComponent<PlayerRopeSwingging>();
        life = GetComponent<PlayerLife>();
        attack = GetComponent<PlayerAttack>();  
        isMoving = true;
        playerAnimator = GetComponent<PlayerAnimator>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (!life.isAlive) return;

        m_InputDirection.x = Input.GetAxis("Horizontal");
        m_InputDirection.y = Input.GetAxis("Vertical");
        bool jumped = Input.GetButtonDown("Jump");
        bool attacked = Input.GetButtonDown("Fire1");

        m_Movement.Move(m_InputDirection);

        if (isMoving)
        {
            if (jumped)
            {
                m_Movement.Jump();
            }

            if (attacked)
            {
                attack.Attack(m_InputDirection.x);
            }
            playerAnimator.SetIsRope(false);
          

        }
        else if (isRopeSwingging)
        {
            m_RopeSwinging.Swing(m_InputDirection);
            if (jumped)
            {
                m_RopeSwinging.Jump();
            }
        }
           
        
    }
    public void SetIsMoving(bool value)
    {
        isMoving = value;
        isRopeSwingging = !value;
        
    }
    public bool GetIsMoving()
    {
        return isMoving;
    }
    public void SetIsRopeSwingging(bool value)
    {
        isRopeSwingging=value;
        isMoving = !value;
        playerAnimator.SetIsRope(true);
    
    }
    public  bool GetIsRopeSwingging()
    {
        return isRopeSwingging;
       
    }
   
}
