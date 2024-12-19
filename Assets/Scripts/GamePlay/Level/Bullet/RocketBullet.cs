using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RocketBullet : BulletBase
{

    [Header("Missile Properties")]
    public float _rotationSpeed = 5f;        // How quickly the missile can turn
    void FixedUpdate()
    {
        if (_target != null)
        {
            // Calculate direction to target
            Vector3 directionToTarget = (_target.position - transform.position).normalized;

            // Smoothly rotate towards the target
            Quaternion rotationToTarget = Quaternion.LookRotation(directionToTarget);
            _rigidbody.rotation = Quaternion.Slerp(_rigidbody.rotation, rotationToTarget, _rotationSpeed * Time.fixedDeltaTime);

            // Move forward
            _rigidbody.velocity = transform.forward * _speed;
        }
    }

    // void FindClosestTarget()
    // {
    //     // Find all potential targets on the specified layer
    //     Collider[] potentialTargets = Physics.OverlapSphere(transform.position, 100f, targetLayer);

    //     float closestDistance = Mathf.Infinity;
    //     Transform closestTarget = null;

    //     // Find the closest target
    //     foreach (Collider targetCollider in potentialTargets)
    //     {
    //         float distance = Vector3.Distance(transform.position, targetCollider.transform.position);
    //         if (distance < closestDistance)
    //         {
    //             closestDistance = distance;
    //             closestTarget = targetCollider.transform;
    //         }
    //     }

    //     _target = closestTarget;
    // }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IDamage>() != null)
        {
            this.OnDespawn();
        }

    }

}
