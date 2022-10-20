using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class PathRequestManager : MonoBehaviour
{
    private Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    private PathRequest currentPathRequest;

    public static PathRequestManager instance;
    private Pathfinding pathfinding;

    private bool isProcessingPath;

    private void Awake()
    {
        instance = this;
        pathfinding = GetComponent<Pathfinding>();
    }

    public static void RequestPath(Vector2 pathStart, Vector2 pathEnd, Action<Vector2[], bool> callback, GameObject owner)
    {
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback, owner);
        instance.pathRequestQueue.Enqueue(newRequest);
        instance.TryProcessNext();
    }

    void TryProcessNext()
    {
        if (!isProcessingPath && pathRequestQueue.Count > 0)
        {
            currentPathRequest = pathRequestQueue.Dequeue();
            isProcessingPath = true;
            pathfinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
        }
    }

    public void FinishedProcessingPath(Vector2[] path, bool success)
    {
        if(currentPathRequest.owner != null) if(currentPathRequest.owner.gameObject.activeSelf) currentPathRequest.callback(path, success);
        isProcessingPath = false;
        TryProcessNext();
    }

    public void ClearQueue()
    {
        instance.pathRequestQueue.Clear();
        currentPathRequest = new PathRequest();
    }

    struct PathRequest
    {
        public Vector2 pathStart;
        public Vector2 pathEnd;
        public Action<Vector2[], bool> callback;
        public GameObject owner;

        public PathRequest(Vector2 _start, Vector2 _end, Action<Vector2[], bool> _callback, GameObject _owner)
        {
            pathStart = _start;
            pathEnd = _end;
            callback = _callback;
            owner = _owner;
        }
    }
}
