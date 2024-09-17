using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    private List<Node> _path;
    private LineRenderer _lineRenderer;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void SetPath(List<Node> path)
    {
        this._path = path;
        ShowPath();
    }

    private void ShowPath()
    {
        _lineRenderer.positionCount = _path.Count;
        for (int i = 0; i < _path.Count; i++)
        {
            this._lineRenderer.SetPosition(i, this._path[i].GetWorldPosition());
        }
        _lineRenderer.numCapVertices = 5;
        _lineRenderer.numCornerVertices = 5;
    }
}