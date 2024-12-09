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
        this._animationControl.PlayAttackAnimation();
        GameObject bulletInstance = this.SpawnBullet(this._bullet, this._attackTranform[0].position);
        bulletInstance.transform.SetParent(this._targetRotation.Turret);
        bulletInstance.transform.localRotation = Quaternion.Euler(Vector3.zero);
        bulletInstance.transform.position = this._attackTranform[0].position;
        bulletInstance.transform.localScale = Vector3.one;
    }
}
