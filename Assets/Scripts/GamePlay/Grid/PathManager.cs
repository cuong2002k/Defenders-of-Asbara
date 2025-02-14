using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Grid))]
[RequireComponent(typeof(PathFinding))]
public class PathManager : Singleton<PathManager>
{
    [Header("path use for enemy moving")]
    private List<Node> _pathsUse = new List<Node>();
    [Header("path use showing when placement")]
    private List<Node> _pathView = new List<Node>();
    public List<Node> Paths => _pathsUse;
    private LineRenderer _lineRenderer;
    private Grid _grid;
    private PathFinding _pathFiding;
    private Vector3 _startPos;
    private Vector3 _endPos;

    protected override void Awake()
    {
        base.Awake();
        InitComponent();
    }


    private void InitComponent()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _grid = GetComponent<Grid>();
        _pathFiding = GetComponent<PathFinding>();
    }

    private void Start()
    {
        this._startPos = LevelManager.Instance.StartPoint;
        this._endPos = LevelManager.Instance.EndPoint;
        UpdatePathView();
        UpdatePathPrimary();
    }

    public void UpdatePathPrimary()
    {
        _grid.UpdateGridNode();
        this._pathsUse = this._pathView;
        ShowPath(_pathsUse);
    }

    public void UpdatePathView()
    {
        _grid.UpdateGridNode();
        _pathFiding.StartFindingPath(this._startPos, this._endPos, ref this._pathView);
        ShowPath(_pathView);
    }

    public void UpdatePathView(Node current, Node old)
    {
        _grid.UpdateGridNode(current, old);
        _pathFiding.StartFindingPath(this._startPos, this._endPos, ref this._pathView);
        ShowPath(_pathView);
    }

    private void ShowPath(List<Node> pathView)
    {
        _lineRenderer.positionCount = pathView.Count;
        for (int i = 0; i < pathView.Count; i++)
        {
            this._lineRenderer.SetPosition(i, pathView[i].GetWorldPosition());
        }
    }
}

