using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
[RequireComponent(typeof(Grid))]
public class PathFinding : MonoBehaviour
{
    private Grid _grid;
    private bool _canMove;
    public bool CanMove => _canMove;

    private void Awake()
    {
        _grid = GetComponent<Grid>();
    }

    public void StartFindingPath(Vector3 startPos, Vector3 endPos, ref List<Node> path)
    {
        _canMove = FindPath(startPos, endPos, ref path);
    }

    private bool FindPath(Vector3 start, Vector3 target, ref List<Node> path)
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
                path = RetracePath(startNode, targetNode);
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

    public List<Node> RetracePath(Node startNode, Node targetNode)
    {
        List<Node> path = new List<Node>();

        Node currentNode = targetNode;
        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Add(startNode);
        path.Reverse();
        return path;
    }

    public int GetDistance(Node nodeA, Node nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distanceY = Mathf.Abs(nodeA.gridY - nodeB.gridY);
        return 1 * (distanceX + distanceY);
    }


}
