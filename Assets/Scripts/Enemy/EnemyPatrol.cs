using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : EnemyBehavior
{
    public List<Vector3> patrolPositions;
    public float distanceChangePosition;

    public float timeRest;

    private EnemyMove movement;

    private float currentTimeRest;
    private int targetIndex;
    private bool isResting;
    private Vector3 initPosition;

    // Start is called before the first frame update
    new protected void Start()
    {
        base.Start();
        movement = GetComponent<EnemyMove>();
        initPosition = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAlive()) { 

            Vector3 newPosition = initPosition + patrolPositions[targetIndex];

            if (Vector3.Distance(newPosition, transform.position) < distanceChangePosition)
            {
                if(targetIndex == patrolPositions.Count - 1)
                {
                    targetIndex = 0;
                }
                else
                {
                    targetIndex++;
                }
                isResting = true;
            }
            if (!isResting) { 
                if(newPosition.x > transform.position.x)
                {
                    movement.Move(1);
                }
                else
                {
                    movement.Move(-1);
                }
            }
            else
            {
                currentTimeRest += Time.deltaTime;
                if(currentTimeRest > timeRest)
                {
                    isResting=false;
                    currentTimeRest = 0;
                }
            }
        }

    }
    void OnDrawGizmosSelected()
    {
        foreach (Vector3 pos in patrolPositions)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(initPosition + pos, 1);
        }
        // Draw a yellow sphere at the transform's position

    }

}
