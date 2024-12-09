using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [SerializeField] private float _radius = 2f;
    [SerializeField] private LayerMask _detectorLayer;

    public GameObject GetClosestEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, _radius, _detectorLayer);
        GameObject enemyClosest = null;
        float minDistance = float.MaxValue;
        Vector3 currentPoint = this.transform.position;
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject == this.gameObject) continue;
            Vector3 direction = currentPoint - collider.transform.position;
            float bestDistance = direction.sqrMagnitude;
            if (bestDistance < minDistance)
            {
                minDistance = bestDistance;
                enemyClosest = collider.gameObject;
            }
        }
        return enemyClosest;
    }

    private void OnDrawGizmosSelected()
    {
        // Gizmos.DrawSphere(this.transform.position, this._radius);
    }
}
