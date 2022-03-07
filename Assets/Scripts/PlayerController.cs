using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    private Vector2 m_InputDirection; 
    private PlayerMovement m_Movement;
    // Start is called before the first frame update
    void Start()
    {
        m_Movement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        m_InputDirection.x = Input.GetAxis("Horizontal");
        m_InputDirection.y = Input.GetAxis("Vertical");
        m_Movement.Move(m_InputDirection);

        if (Input.GetButtonDown("Jump"))
        {
            m_Movement.Jump();
        }
    }

    
}
