using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PathManager : Singleton<PathManager>
{
  
    private List<Node> _path = new List<Node>();
    public List<Node> Paths => _path;
    private LineRenderer _lineRenderer;
    private Grid _grid;
    private PathFinding _pathFiding;

    protected override void Awake()
    {
        base.Awake();
        _lineRenderer = GetComponent<LineRenderer>();
        _grid = GetComponent<Grid>();
        _pathFiding = GetComponent<PathFinding>();
        
    }

    private void Start()
    {
      UpdatePath(_grid.NodeFromWorldPoint(LevelManager.Instance.StartPoint));

    }

    public void SetPath(List<Node> path)
    {
        this._path = path;
        ShowPath();
    }

    public void UpdatePath(Node currentNode)
    {
        Vector3 startPoint = LevelManager.Instance.StartPoint;
        Vector3 endPoint = LevelManager.Instance.EndPoint;
        _grid.UpdateGridNode(currentNode);
        _pathFiding.StartFindingPath(startPoint, endPoint, ref _path);
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