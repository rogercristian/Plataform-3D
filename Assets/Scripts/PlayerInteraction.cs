using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    PlayerMovement m_Movement;

    private void Start()
    {
            m_Movement = GetComponent<PlayerMovement>();
    }
    private void OnTriggerEnter(Collider other)
    {
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
}
