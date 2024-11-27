using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Fire : TowerBase
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Shoot()
    {
        GameObject bulletInstance = this.SpawnBullet(this._bullet, this._attackTranform[0].position);
        bulletInstance.transform.SetParent(this._targetRotation.Turret);
        bulletInstance.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }
}
