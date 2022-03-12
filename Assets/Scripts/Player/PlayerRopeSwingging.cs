using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRopeSwingging : MonoBehaviour
{
    public float swingForce;
    private RopeBehavior rope;
    private PlayerMovement movement;
    PlayerController controller;
    private PlayerInteraction interaction;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        movement = GetComponent<PlayerMovement>(); 
        controller = GetComponent<PlayerController>();
        interaction = GetComponent<PlayerInteraction>();    
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AttachRope(RopeBehavior rope, GameObject node)
    {
        this.rope = rope;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rope.AttachPlayer(this, node);
    }

    public void Swing(Vector3 input)
    {
        if(rope != null)
        {
            rope.ApplySwing(new Vector3(input.x * swingForce * Time.deltaTime, 0f, 0f));
        }
    }

    public void Jump()
    {
        interaction.SetIsJumpingRope();
        rb.useGravity = true;
        rope.DetachPlayer();
        movement.Jump();
        rope = null;
        controller.SetIsMoving(true);
       
    }

}
