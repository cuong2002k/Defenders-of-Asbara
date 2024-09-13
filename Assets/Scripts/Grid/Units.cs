using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Units : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] Vector3[] path;
    [SerializeField] private int targetIndex = 0;
    Vector3 currentWayPoint;
    private void Start()
    {

    }

    private void Update()
    {
        if (targetIndex >= this.path.Length && this.path.Length != 0)
        {
            Destroy(this.gameObject);
            return;
        }
        if (this.transform.position == currentWayPoint)
        {
            targetIndex++;
            if (targetIndex < this.path.Length)
            {
                currentWayPoint = this.path[targetIndex];
            }
        }
        this.transform.position = Vector3.MoveTowards(this.transform.position, currentWayPoint, this._speed * Time.deltaTime);
    }

    public void RequestPath(Transform target)
    {
        PathRequestManager.RequestPath(this.transform.position, target.position, OnPathFound);
    }

    private void OnPathFound(Vector3[] path, bool pathSucessFul)
    {
        if (pathSucessFul)
        {
            this.path = path;
            targetIndex = 0;
            currentWayPoint = this.path[0];
            // StopCoroutine(FollowPath());
            // StartCoroutine(FollowPath());
        }
    }

    // private IEnumerator FollowPath()
    // {
    //     Vector3 currentWayPoint = this.path[0];
    //     while (true)
    //     {
    //         if (this.transform.position == currentWayPoint)
    //         {
    //             targetIndex++;

    //             currentWayPoint = this.path[targetIndex];
    //         }
    //         this.transform.position = Vector3.MoveTowards(this.transform.position, currentWayPoint, this._speed * Time.deltaTime);
    //         yield return null;
    //     }
    // }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}
