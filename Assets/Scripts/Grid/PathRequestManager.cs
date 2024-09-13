using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathRequestManager : MonoBehaviour
{
    public static PathRequestManager instance;
    private Queue<PathRequest> _pathRequestQueue = new Queue<PathRequest>();
    private PathRequest _currentPathRequest;
    private bool _isProcessingPath;

    private PathFinding _pathFinding;

    private void Awake()
    {
        instance = this;
        _pathFinding = GetComponent<PathFinding>();
    }


    public static void RequestPath(Vector3 startPos, Vector3 endPos, Action<Vector3[], bool> callBack)
    {
        PathRequest newPath = new PathRequest(startPos, endPos, callBack);
        instance._pathRequestQueue.Enqueue(newPath);
        instance.TryNextProcess();
    }

    private void TryNextProcess()
    {
        if (!_isProcessingPath && this._pathRequestQueue.Count > 0)
        {
            _currentPathRequest = this._pathRequestQueue.Dequeue();
            _isProcessingPath = true;
            _pathFinding.StartFindingPath(_currentPathRequest.startPos, _currentPathRequest.endPos);
        }
    }

    public void FinishProcessingPath(Vector3[] path, bool sucess)
    {
        this._currentPathRequest.callBack(path, sucess);
        this._isProcessingPath = false;
        this.TryNextProcess();
    }

    class PathRequest
    {
        private Vector3 _startPos;
        public Vector3 startPos => _startPos;
        private Vector3 _endPos;
        public Vector3 endPos => _endPos;

        private Action<Vector3[], bool> _callBack;
        public Action<Vector3[], bool> callBack => _callBack;

        public PathRequest(Vector3 startPos, Vector3 endPos, Action<Vector3[], bool> callBack)
        {
            this._startPos = startPos;
            this._endPos = endPos;
            this._callBack = callBack;
        }
    }
}
