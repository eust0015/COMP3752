using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourMelee : MonoBehaviour
{
    private Unit u;
    private bool tracking;
    [SerializeField] private float maxDistance = 5f;
    void Start()
    {
        u = GetComponent<Unit>();
        tracking = false;
        StartCoroutine(Condition());
    }

    IEnumerator Condition()
    {
        while (true)
        {
            if (!tracking && u.targetDistance > maxDistance)
            {
                print("tracking");
                u.StartPath();
                tracking = true;
            }

            if (u.targetDistance < maxDistance)
            {
                print("not tracking");
                u.StopPath();
                tracking = false;
            }

            yield return null;
        }
    }
}
