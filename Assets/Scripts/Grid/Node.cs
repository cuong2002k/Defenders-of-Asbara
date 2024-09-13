using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node>
{
    private bool _walkAble;
    private Vector3 _worldPosition;
    private float _gCost; // save all movement cost from start to current node
    private float _hCost; // estimated movement cost from current node to target
    public float gCost
    {
        get { return _gCost; }
        set { _gCost = value; }
    }
    public float hCost
    {
        get { return _hCost; }
        set { _hCost = value; }
    }
    public float fCost
    {
        get
        {
            return _hCost + _gCost;
        }
    }

    private int _gridX;
    private int _gridY;
    public int gridX
    {
        get { return _gridX; }
        set { _gridX = value; }
    }
    public int gridY
    {
        get { return _gridY; }
        set { _gridY = value; }
    }

    private Node _parent;
    public Node parent
    {
        get { return _parent; }
        set { _parent = value; }
    }

    public Vector3 GetWorldPosition() => this._worldPosition;

    public bool walkAble
    {
        get
        {
            return this._walkAble;
        }
        set
        {
            this._walkAble = value;
        }
    }

    private int _heapIndex;
    public int HeapIndex
    {
        get
        {
            return this._heapIndex;
        }
        set
        {
            this._heapIndex = value;
        }
    }

    public Node(bool canWalkAble, Vector3 worldPosition, int gridX,
    int gridY)
    {
        this._walkAble = canWalkAble;
        this._worldPosition = worldPosition;
        this._gridX = gridX;
        this._gridY = gridY;
    }

    public int CompareTo(Node nodeCompare)
    {
        int fCostCompare = this.fCost.CompareTo(nodeCompare.fCost);
        if (fCost == 0)
        {
            fCostCompare = this.hCost.CompareTo(nodeCompare.hCost);
        }
        return -fCostCompare;
    }

}
