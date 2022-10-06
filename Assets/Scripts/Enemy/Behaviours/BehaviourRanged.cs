using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static Unity.Mathematics.math;

public class BehaviourRanged : Behaviour
{
    private bool los;
    [SerializeField]
    private float minimumMovementTime = 0f;

    [SerializeField] private float shotDelay;
    private float curDelay;
    [SerializeField] private float chargeTime;

    [SerializeField] private float projSpeed;
    public Sprite _s;
    

    private float moveTime;
    private void FixedUpdate()
    {
        los = u.lineOfSight;
        if (!tracking)
        {
            Debug.Log(u.target.position);
            var theta = getAngleFromVectors(new Vector2(u.target.position.x, u.target.position.y));
            Debug.Log(theta - 90);
            transform.localEulerAngles += new Vector3(0, 0, theta - 90);
        }
        if(los) moveTime -= Time.deltaTime;
        curDelay -= Time.deltaTime;

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
                if (curDelay <= 0) StartCoroutine(Attack());
                tracking = false;
                yield return null;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    protected override IEnumerator Attack()
    {
        if (_a == null) yield break;

        yield return new WaitForSeconds(chargeTime);
        _a.RequestProjectile(new Projectile(2f, _s, 3, 4));
        curDelay = shotDelay;
    }
    private float getAngleFromVectors(Vector2 lookPoint)
    {
        Vector2 rotation = (lookPoint - new Vector2(transform.position.x, transform.position.y) ).normalized;
        Vector2 current = new Vector2(Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad),
            Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad));
        float dot = (rotation.x * current.x) + (rotation.y * current.y);
        float mag = Mathf.Sqrt((rotation.x * rotation.x) + (rotation.y * rotation.y)) * Mathf.Sqrt((current.x * current.x) + (current.y * current.y));
        float theta = math.degrees(Mathf.Acos(dot / mag));
        return theta;
    }
}
