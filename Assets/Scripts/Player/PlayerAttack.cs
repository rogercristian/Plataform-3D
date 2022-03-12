using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRate;
    public int damage;
    public float dashForce;
    public float dashingTime;
    public TrailRenderer trail;

    float currentAttackRate;
    float currentDashingTime;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentAttackRate = attackRate;
        currentDashingTime = dashingTime;
       // trail = GetComponentInChildren<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        currentAttackRate += Time.deltaTime;
        currentDashingTime += Time.deltaTime;

        if (!IsAttacking())
        {
            trail.gameObject.SetActive(false);
        }
    }

    public void Attack(float direction)
    {
        if (currentAttackRate > attackRate && direction != 0)
        {
            currentAttackRate = 0;
            currentDashingTime = 0;
            direction = direction > 0? 1 : -1;
            rb.AddForce(direction * dashForce, 0f, 0f);
            trail.gameObject.SetActive(true);

        }
    }

    public bool IsAttacking()
    {
        return currentDashingTime < dashingTime;
    }
}
