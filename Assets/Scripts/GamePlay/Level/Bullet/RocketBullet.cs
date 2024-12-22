using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class RocketBullet : BulletBase
{

    [Header("Missile Properties")]
    public float _rotationSpeed = 5f;        // How quickly the missile can turn
    private float _radius = 2f;
    public LayerMask _enemyLayer;

    [SerializeField] private Collider[] Hits;

    void FixedUpdate()
    {
        if (_target != null)
        {
            // Calculate direction to target
            Vector3 directionToTarget = (_target.position - transform.position).normalized;

            // Smoothly rotate towards the target
            Quaternion rotationToTarget = Quaternion.LookRotation(directionToTarget);
            _rigidbody.rotation = Quaternion.Slerp(_rigidbody.rotation, rotationToTarget, _rotationSpeed * Time.deltaTime);

            // Move forward
            _rigidbody.velocity = transform.forward * _speed;
        }
        else
        {
            this.OnDespawn();
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        Vector3 contactPoint = other.ClosestPoint(this.transform.position);
        if (this._hitEffect != null)
        {
            GameObject hitFX = PoolAble.TryGetPool(this._hitEffect);
            hitFX.transform.position = contactPoint;
        }

        this.Hits = Physics.OverlapSphere(contactPoint, this._radius, _enemyLayer);

        for (int i = 0; i < Hits.Length; i++)
        {
            float distance = Vector3.Distance(contactPoint, Hits[i].transform.position);

            int finalDamage = Mathf.FloorToInt(Mathf.Lerp(20, this._damage, this._damage / _radius));

            IDamage damage = Hits[i].GetComponent<IDamage>();
            if (damage != null)
            {
                damage.TakeDamage(finalDamage);
            }
        }


        this.OnDespawn();

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(this.transform.position, this._radius);
    }

}
