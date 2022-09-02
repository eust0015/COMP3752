using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourMelee : Behaviour
{

    protected override IEnumerator Condition()
    {
        while (true)
        {
            if (!tracking && u.targetDistance > maxDistance)
            {
                print("tracking");
                tracking = true;
                u.StartPath();
                yield return null;

            }

            if (u.targetDistance < maxDistance)
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
