using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : TowerBase
{
    protected override void Shoot()
    {
        if (this._bullet == null)
        {
            Common.LogWarning("Bullet prefab not found in {0}", this.gameObject);
            return;
        }

        this._animationControl.PlayAttackAnimation();
        Transform firstTarget = this._targetter.GetFirstTarget();
        GameObject bulletInstance = this.SpawnBullet(this._bullet, this._attackTranform[0].position);
        bulletInstance.GetComponent<BulletBase>().SetTarget(firstTarget);
        bulletInstance.GetComponent<IPoolAble>().OnSpawn();

    }
}
