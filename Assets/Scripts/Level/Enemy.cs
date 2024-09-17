using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Node startNode;
    private List<Node> _agent = new List<Node>();
    private int _pathIndex = 0;
    private float _speed = 10f;

    public void SetPath(List<Node> agent)
    {
        this._agent = agent;
    }

    void Update()
    {
        if(_pathIndex >= _agent.Count) return;
        Vector3 positionMove = _agent[_pathIndex].GetWorldPosition();
        float distance = Vector3.Distance(this.transform.position, positionMove);
        if(distance <= 0.1f)
        {
            _pathIndex++;
        }
        this.transform.position = Vector3.MoveTowards(this.transform.position, positionMove, _speed * Time.deltaTime);
        
    }
}
