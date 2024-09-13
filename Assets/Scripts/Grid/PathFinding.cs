using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
public class PathFinding : MonoBehaviour
{
    private Grid _grid;
    private Vector3[] _wayPoint = new Vector3[0];
    public Vector3[] wayPoint => _wayPoint;
    private bool _canMove;
    public bool canMove => _canMove;

    private PathManager _pathManager;

    private void Awake()
    {
        _grid = GetComponent<Grid>();
        _wayPoint = new Vector3[0];

    }

    private void Start()
    {
        _pathManager = GetComponent<PathManager>();
    }

    public void StartFindingPath(Vector3 startPos, Vector3 endPos)
    {
        // StartCoroutine(FindPath(startPos, endPos));
        _canMove = FindPath(startPos, endPos);
    }

    private bool FindPath(Vector3 start, Vector3 target)
    {
        Node startNode = _grid.NodeFromWorldPoint(start);
        Node targetNode = _grid.NodeFromWorldPoint(target);

        Heap<Node> openSet = new Heap<Node>(_grid.MaxGridSize);
        HashSet<Node> closedSet = new HashSet<Node>(); // save all visited node
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet.GetFirst();
            openSet.RemoveFirst();

            closedSet.Add(currentNode); // currentNode => visited
            if (currentNode == targetNode)
            {
                _wayPoint = RetracePath(startNode, targetNode);
                
                return true;
            }

            foreach (Node neighbour in _grid.GetNeighbours(currentNode))
            {
                if (!neighbour.walkAble || closedSet.Contains(neighbour))
                {
                    continue;
                }

                float newMovementCost = currentNode.gCost + GetDistance(currentNode, neighbour);
                if (newMovementCost < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCost;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;
                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }
        return false;
    }

    public Vector3[] RetracePath(Node startNode, Node targetNode)
    {
        List<Node> path = new List<Node>();

        Node currentNode = targetNode;
        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Add(startNode);
        _grid.path = path;
        _pathManager.SetPath(path);
        Vector3[] wayPoint = SimplifyPath(path);
        Array.Reverse(wayPoint);
        
        return wayPoint;
    }

    private Vector3[] SimplifyPath(List<Node> path)
    {
        List<Vector3> wayPoint = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;
        for (int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if (directionNew != directionOld)
            {
                wayPoint.Add(path[i].GetWorldPosition());
            }
            directionOld = directionNew;
        }
        return wayPoint.ToArray();
    }

    // public int GetDistance(Node nodeA, Node nodeB)
    // {
    //     int distanceX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
    //     int distanceY = Mathf.Abs(nodeA.gridY - nodeB.gridY);
    //     if (distanceX > distanceY)
    //         return 14 * distanceY + 10 * (distanceX - distanceY);
    //     return 14 * distanceX + 10 * (distanceY - distanceX);
    // }

    public int GetDistance(Node nodeA, Node nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distanceY = Mathf.Abs(nodeA.gridY - nodeB.gridY);
        return 1 * (distanceX + distanceY);
    }


}
