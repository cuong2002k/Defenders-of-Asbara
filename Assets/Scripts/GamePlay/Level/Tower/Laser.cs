using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Laser : TowerBase
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Shoot()
    {
        Transform attackTranform = this.GetFirstAttackPoint();
        GameObject bulletInstance = this.SpawnPrefabs(this._bulletPrefabs, attackTranform.position);
        bulletInstance.GetComponent<BulletBase>().Initialized(this.finalDamage);
        bulletInstance.transform.SetParent(this._targetRotation.Turret);
        bulletInstance.transform.localRotation = Quaternion.Euler(Vector3.zero);
        bulletInstance.transform.position = attackTranform.position;
        bulletInstance.transform.localScale = Vector3.one;
    }
}
