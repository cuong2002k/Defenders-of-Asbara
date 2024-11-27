using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : BulletBase
{
    private void OnTriggerEnter(Collider other)
    {
        IDamage damage = other.GetComponent<IDamage>();

        if (damage != null)
        {
            damage.TakeDamage(this._damage);
        }
    }

    private void OnTriggerExit(Collider other)
    {

    }
}
