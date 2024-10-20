using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Grid))]
[RequireComponent(typeof(PathFinding))]
public class PathManager : Singleton<PathManager>
{
    
    private List<Node> _path = new List<Node>();
    public List<Node> Paths => _path;
    private LineRenderer _lineRenderer;
    private Grid _grid;
    private PathFinding _pathFiding;
    private Vector3 _startPos;
    private Vector3 _endPos;

    protected override void Awake()
    {
        base.Awake();
        _lineRenderer = GetComponent<LineRenderer>();
        _grid = GetComponent<Grid>();
        _pathFiding = GetComponent<PathFinding>();
    }

    private void Start()
    {
      this._startPos = LevelManager.Instance.StartPoint;
      this._endPos = LevelManager.Instance.EndPoint;
      UpdatePath();

    }

    public void SetPath(List<Node> path)
    {
        this._path = path;
        ShowPath();
    }

    public void UpdatePath()
    {
        _grid.UpdateGridNode();
        _pathFiding.StartFindingPath(this._startPos, this._endPos, ref _path);
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