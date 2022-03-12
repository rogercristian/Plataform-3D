using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : DestructbleBeahavior
{
    public float positionOffSet;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        Vector3 newPosition = transform.position;
        newPosition.y -= positionOffSet;
        transform.position = newPosition;
    }
}
