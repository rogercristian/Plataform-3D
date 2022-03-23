using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public float timeInvencible;
    public int maxLife;
    public float blinkingTime;
    //public int minLife;
    public bool isAlive = true;
    public Transform model;

    private int life;
    private float currentTimeInvencible;
    private float currentTimeBlinking;
    private Renderer[] m_Renderer;
    private PlayerAttack attack;

    private PlayerAnimator playerAnimator;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        life = maxLife;
        currentTimeInvencible = timeInvencible;
        m_Renderer = model.GetComponentsInChildren<Renderer>();
        attack = GetComponent<PlayerAttack>();
        playerAnimator = GetComponent<PlayerAnimator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTimeInvencible += Time.deltaTime;
       
        if (IsInvencible() && !attack.IsAttacking())
        { 
            currentTimeBlinking +=  Time.deltaTime;

            if(currentTimeBlinking > blinkingTime)
            {
                foreach(Renderer r in m_Renderer) {
                    r.enabled = !r.enabled;
                }
               
                currentTimeBlinking = 0;
            } 

        }else 
            {
            foreach (Renderer r in m_Renderer)
            {
                r.enabled = true;
            }
        }

        if (!isAlive)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);

        }
    }

    public void ApplyDamage()
    {
        if(!IsInvencible() && isAlive) 
        {
            life --;
            currentTimeInvencible = 0;

            if (life < 1)
            {
                isAlive = false;
                playerAnimator.SetIsDead();
            }
        }
    }

    bool IsInvencible()
    {
        return currentTimeInvencible < timeInvencible || attack.IsAttacking();
    }
}
