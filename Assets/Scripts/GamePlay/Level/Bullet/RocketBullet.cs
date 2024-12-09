using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RocketBullet : BulletBase
{
    [SerializeField] private GameObject _muzzleEffect;
    private float _turnSpeed = 2f;

    public override void OnSpawn()
    {
        base.OnSpawn();


    }


    private void FixedUpdate()
    {
        if (this._target == null) return;
        this._rigidbody.velocity = this.transform.forward * this._speed;

        var rocketRotation = Quaternion.LookRotation(this._target.position - this.transform.position);

        _rigidbody.MoveRotation(Quaternion.RotateTowards(this.transform.rotation, rocketRotation, this._turnSpeed));

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IDamage>() != null)
        {
            this.OnDespawn();
        }

    }
}
