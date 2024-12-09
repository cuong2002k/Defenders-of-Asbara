using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LaserBullet : BulletBase
{

    private void OnTriggerEnter(Collider other)
    {
        IDamage damage = other.GetComponent<IDamage>();

        if (damage == null) return;
        damage.TakeDamage(this._damage);
        if (this._hitEffect == null) return;
        Vector3 contactPoint = other.ClosestPoint(this.transform.position);
        GameObject hitFX = PoolAble.TryGetPool(this._hitEffect);
        hitFX.transform.position = contactPoint;


    }
}
