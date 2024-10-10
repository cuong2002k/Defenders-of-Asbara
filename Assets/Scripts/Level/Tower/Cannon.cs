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
    CannonBullet bullet = Instantiate(this._bullet, _fireTranform.position, Quaternion.identity).GetComponent<CannonBullet>();
    if(bullet != null)
    {
      bullet.SetTarget(this._targetAble.Target);
    }
  }
}
