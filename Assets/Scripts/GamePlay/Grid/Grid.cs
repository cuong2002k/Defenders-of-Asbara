using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private int _gridHeight;
    [SerializeField] private int _gridWidth;
    [SerializeField] private LayerMask _unWalkAble;
    private Node[,] _grid;
    [SerializeField] private float _nodeRadius = 0.5f;
    private float _nodeDiameter;
    private int _gridSizeX;
    private int _gridSizeY;
    public List<Node> path;

    public int MaxGridSize
    {
        get
        {
            return _gridSizeX * _gridSizeY;
        }
    }

    private void Awake()
    {
        path = new List<Node>();
        _nodeDiameter = _nodeRadius * 2;
        _gridSizeX = Mathf.RoundToInt(_gridHeight / _nodeDiameter);
        _gridSizeY = Mathf.RoundToInt(_gridWidth / _nodeDiameter);
        CreateGrid();

    }

    private void CreateGrid()
    {
        _grid = new Node[_gridSizeX, _gridSizeY];
        Vector3 worldBottomLeft = this.transform.position - Vector3.right * _gridWidth / 2 - Vector3.forward * _gridHeight / 2;
        for (int x = 0; x < _gridSizeX; x++)
        {
            for (int y = 0; y < _gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * _nodeDiameter + _nodeRadius) + Vector3.forward * (y * _nodeDiameter + _nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, _nodeRadius - 0.1f, _unWalkAble));
                _grid[x, y] = new Node(walkable, worldPoint, x, y);

            }
        }
    }

    public void UpdateGridNode()
    {
      for(int x = 0; x < this._gridSizeX; x++)
      {
        for(int y = 0; y < this._gridSizeY; y++)
        {
          bool walkable = !(Physics.CheckSphere(_grid[x,y].GetWorldPosition(), _nodeRadius - 0.1f, _unWalkAble));
          _grid[x, y].walkAble = walkable;
        }
      }
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();
        for (int i = -1; i <= 1; i++)
        {
            if (i == 0) continue;
            int x = node.gridX + i;
            int y = node.gridY;
            if (x >= 0 && x < _gridSizeX && y >= 0 && y < _gridSizeY)
            {
                neighbours.Add(_grid[x, y]);
            }
        }

        for (int i = -1; i <= 1; i++)
        {
            if (i == 0) continue;
            int x = node.gridX;
            int y = node.gridY + i;
            if (x >= 0 && x < _gridSizeX && y >= 0 && y < _gridSizeY)
            {
                neighbours.Add(_grid[x, y]);
            }
        }

        return neighbours;
    }

    public Node NodeFromWorldPoint(Vector3 worldPoint)
    {
        float percentX = (worldPoint.x / _gridWidth) + 0.5f;
        float percentY = (worldPoint.z / _gridHeight) + 0.5f;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);
        int x = Mathf.RoundToInt((_gridWidth - 1) * percentX);
        int y = Mathf.RoundToInt((_gridHeight - 1) * percentY);
        return _grid[x, y];

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(this.transform.position, new Vector3(this._gridWidth, 0.5f, this._gridHeight));

        if (_grid != null)
        {
            foreach (Node node in _grid)
            {
                Gizmos.color = (node.walkAble) ? Color.white : Color.red;
                if (path != null)
                {
                    if (path.Contains(node))
                        Gizmos.color = Color.black;
                }
                Vector3 nodeSize = Vector3.one * (_nodeDiameter - 0.1f);
                nodeSize.y = 0.2f;
                Gizmos.DrawCube(node.GetWorldPosition(), nodeSize);
            }
        }
    }

}
