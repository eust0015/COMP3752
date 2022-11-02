using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

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

    private void OnDisable()
    {
        StopPath();
    }

    private void OnEnable()
    {
        StartPath();
    }

    public bool lineOfSight
    {
        get
        {
            RaycastHit2D hit;
            hit = Physics2D.Raycast(transform.position, target.position - transform.position);
            return hit.collider.gameObject.CompareTag("Player");
        }
    }

    private void Awake()
    {
        if (target == null) target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void OnPathFound(Vector2[] waypoints, bool pathSuccessful)
    {
        if (pathSuccessful && waypoints.Length > 0)
        {
            path = new Path(waypoints, transform.position, turnDst);
            //Debug.Log(gameObject.activeSelf);
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
        
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound, gameObject);
        float sqMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
        Vector3 targetPosOld = target.position;
        
        while (true)
        {
            yield return new WaitForSeconds(minPathUpdateTime);
            if ((target.position - targetPosOld).sqrMagnitude > sqMoveThreshold && upd)
            {
                PathRequestManager.RequestPath(transform.position, target.position, OnPathFound, gameObject);
                targetPosOld = target.position;
            }
        }
    }
    
    public IEnumerator FollowPath()
    {
        bool followingPath = true;
        int pathIndex = 0;
        
        
        while (followingPath)
        {
            if (lineOfSight)
            {
                //Debug.Log("los");
                var theta = getAngleFromVectors(target.position);
                transform.eulerAngles += new Vector3(0, 0, theta - 90);
                transform.Translate(Vector3.up * Time.deltaTime * speed, Space.Self);
                yield return null;
                continue;
            }
            
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
                var theta = getAngleFromVectors(path.lookPoints[pathIndex]);
                float z = Mathf.Lerp(transform.localEulerAngles.z, transform.localEulerAngles.z + (theta - 90), turnSpeed * Time.deltaTime);
                transform.eulerAngles = new Vector3(0, 0,z);
                transform.Translate (Vector3.up * Time.deltaTime * speed, Space.Self);
            }
            yield return null;
        }
    }

    public void StopPath()
    {
        if (!gameObject.activeSelf) return;
        StopCoroutine(UpdatePath());
        upd = false;
        StopCoroutine("FollowPath");
        path = null;
    }

    public void StartPath()
    {
        if (!gameObject.activeSelf) return;
        StartCoroutine(UpdatePath());
        upd = true;
    }

    private void OnDrawGizmos()
    {
        path?.DrawWithGizmos();
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
