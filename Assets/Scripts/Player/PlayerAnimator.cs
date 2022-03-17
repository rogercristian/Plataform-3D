using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void SetIsDead(string value)
    {
        animator.SetTrigger(value);
    }
}
