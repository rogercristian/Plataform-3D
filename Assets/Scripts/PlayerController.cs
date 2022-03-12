using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    private Vector2 m_InputDirection; 
    private PlayerMovement m_Movement;
    private  PlayerRopeSwingging m_RopeSwinging;

    private bool isMoving;

    private bool isRopeSwingging;


    // Start is called before the first frame update
    void Start()
    {
        m_Movement = GetComponent<PlayerMovement>();
        m_RopeSwinging = GetComponent<PlayerRopeSwingging>();
        isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        m_InputDirection.x = Input.GetAxis("Horizontal");
        m_InputDirection.y = Input.GetAxis("Vertical");
        bool jumped = Input.GetButtonDown("Jump");

        m_Movement.Move(m_InputDirection);

        if (isMoving)
        {
            if (jumped)
            {
                m_Movement.Jump();
            }
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
    }
    public  bool GetIsRopeSwingging()
    {
        return isRopeSwingging;
       
    }
   
}
