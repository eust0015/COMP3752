using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class BehaviourMelee : EnemyBehaviour
{
    [SerializeField] private float delay;
    private float curDelay = 0;
    [SerializeField] private float recoverTime;
    [SerializeField] private float leapSpd;
    [SerializeField] private float travelTime;
    private bool allowTrack = true;
    
    protected override IEnumerator Condition()
    {
        while (true)
        {
            if (!tracking && u.targetDistance > maxDistance && allowTrack)
            {
                tracking = true;
                u.StartPath();
                yield return null;

            }

            if (u.targetDistance < maxDistance && allowTrack)
            {
                u.StopPath();
                if(curDelay <= 0) StartCoroutine(Attack());
                tracking = false;
                yield return null;
            }
            
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void Update()
    {
        curDelay -= Time.deltaTime;
    }

    protected override IEnumerator Attack()
    {
        if (_a == null) yield break;

        allowTrack = false;

        var n = (u.target.position - transform.position).normalized;
        var pos = u.target.position - (n * 0.7f);
        
        yield return new WaitForSeconds(delay);
        transform.DOMove(pos, travelTime, false);
        yield return new WaitForSeconds(travelTime / 1.5f);
        _a.RequestHitbox(new Hitbox(1.5f, 1.5f, 0.2f, 2, n * 1.2f));
        
        curDelay = delay;
        yield return new WaitForSeconds(recoverTime);
        allowTrack = true;

    }
    
}
