using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
 
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }


    public void SetIsWalking(bool value)
    {
        animator.SetBool("IsWalking", value);
    }
    public void SetIsJumping(bool value)
    {
        animator.SetBool("IsJumping", value);
    }
    
    public void SetIsSwinning(bool value)
    {
        animator.SetBool("IsSwin", value);
    }

    public void SetIsDashing(bool value)
    {
        animator.SetBool("IsDashing", value);
    }

    public void SetIsRope(bool value)
    {
        animator.SetBool("IsRope", value);
    }

    public void SetIsDead()
    {
        animator.SetTrigger("IsDead");
    }
}
