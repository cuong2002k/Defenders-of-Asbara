using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : TowerBase
{
  protected override void Shoot()
  {
    StartCoroutine(Attack());
  }

  IEnumerator Attack()
  {
    for(int i = 0; i < this._towerData.TargetNumber; i++)
    {
      Transform fireTranform = this._fireTranform[i];
      if(fireTranform != null)
      {
        foreach(Transform target in this._target)
        {
          if(target != null)
          {
            BulletBase bulletBase = this.SpawnBullet(this._bullet, fireTranform.position).GetComponent<BulletBase>();
            bulletBase.SetTarget(target);
          }
        }
      }
      yield return new WaitForSeconds(0.5f);
    }
    yield return null;
  }


}
