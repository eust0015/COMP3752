using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourRanged : Behaviour
{
    private bool los;
    [SerializeField]
    private float minimumMovementTime = 0f;

    private float moveTime;
    private void FixedUpdate()
    {
        los = u.lineOfSight;
        if (!tracking)
        {
            Quaternion targetRotation = Quaternion.LookRotation(new Vector2(u.target.position.x, u.target.position.y) - new Vector2(transform.position.x, transform.position.y));
            transform.rotation = targetRotation;
        }
        if(los) moveTime -= Time.deltaTime;
        
    }

    protected override IEnumerator Condition()
    {
        while (true)
        {
            if (!tracking && !los)
            {
                print("tracking");
                tracking = true;
                u.StartPath();
                moveTime = minimumMovementTime;
                yield return null;
                
            }

            if (los && moveTime < 0)
            {
                print("not tracking");
                u.StopPath();
                tracking = false;
                yield return null;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}
