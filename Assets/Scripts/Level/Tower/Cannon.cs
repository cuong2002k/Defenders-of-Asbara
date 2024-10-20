using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : TowerBase
{
  protected override void Shoot()
  {
    if(this._bullet == null)
    {
      Common.LogWarning("Bullet prefab not found in {0}", this.gameObject);
      return;
    }

    GameObject bulletInstance = this.SpawnBullet(this._bullet, this._fireTranform[0].position);
    bulletInstance.GetComponent<BulletBase>().SetTarget(this._target[0]);
    
  }
}
