using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private const float minPathUpdateTime = .2f;
    [SerializeField]
    private float pathUpdateMoveThreshold = .3f;

    public bool upd = true;

    public Transform target;
    public float speed = 1;
    public float turnSpeed = 3f;
    public float turnDst = 0.5f;

    public Transform spriteTransform;
    private Path path;

    public float targetDistance => Vector2.Distance(transform.position, target.position);

    public bool lineOfSight
    {
        get
        {
            RaycastHit2D hit;
            hit = Physics2D.Raycast(transform.position, target.position - transform.position);
            return hit.collider.gameObject.CompareTag("Player");
        }
    }

    public void OnPathFound(Vector2[] waypoints, bool pathSuccessful)
    {
        if (pathSuccessful && waypoints.Length > 0)
        {
            path = new Path(waypoints, transform.position, turnDst);
            
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    public IEnumerator UpdatePath()
    {
        if (Time.timeSinceLevelLoad < 0.3f)
        {
            yield return new WaitForSeconds(0.3f);
        }
        
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
        float sqMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
        Vector3 targetPosOld = target.position;
        
        while (true)
        {
            print("updating");
            yield return new WaitForSeconds(minPathUpdateTime);
            if ((target.position - targetPosOld).sqrMagnitude > sqMoveThreshold && upd)
            {
                PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
                targetPosOld = target.position;
            }
        }
    }
    
    public IEnumerator FollowPath()
    {
        bool followingPath = true;
        int pathIndex = 0;
        transform.LookAt(path.lookPoints[0]);
        
        while (followingPath)
        {
            Vector2 pos2D = new Vector2(transform.position.x, transform.position.y);
            while (path.turnBoundaries[pathIndex].HasCrossedLine((pos2D)))
            {
                if (pathIndex == path.finishLineIndex)
                {
                    followingPath = false;
                    break;
                }
                else pathIndex++;
            }

            if (followingPath)
            {
                Quaternion targetRotation = Quaternion.LookRotation(path.lookPoints[pathIndex] - new Vector2(transform.position.x, transform.position.y));
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
                //spriteTransform.localEulerAngles = new Vector3(0, 90, 0);
                transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);
            }
            yield return null;
        }
    }

    public void StopPath()
    {
        StopCoroutine(UpdatePath());
        upd = false;
        StopCoroutine("FollowPath");
        path = null;
    }

    public void StartPath()
    {
        StartCoroutine(UpdatePath());
        upd = true;
    }

    private void OnDrawGizmos()
    {
        path?.DrawWithGizmos();
    }
}
