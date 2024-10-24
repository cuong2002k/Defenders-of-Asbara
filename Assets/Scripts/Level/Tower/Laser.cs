using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Laser : TowerBase
{
  LineRenderer _lineRenderer;

  protected override void Start() {
    base.Start();
    this._lineRenderer = this._bullet.GetComponent<LineRenderer>();
    this._lineRenderer.positionCount = 2;
  }

  protected override void Shoot()
  {
    IDamage damage = this._target[0].GetComponent<IDamage>();

    if(damage != null)
    {
      damage.TakeDamage(Mathf.RoundToInt(this._attackPerSecond));
    }
    
  }

  protected override void Update()
  {
    base.Update();
    if(this._targetAble.CheckTarget() == null)
    {
      HideLaser();
      return;
    } 

    LaserAttack();

  }

  private void LaserAttack()
  {
    ShowLaser();
    for(int i = 0; i < this._towerData.TargetNumber; i++)
    {
      if(this._target[i] != null)
      {
        this._lineRenderer.SetPosition(0, this._fireTranform[i].position);
        this._lineRenderer.SetPosition(1, this._target[i].position);
      }
    }
  }

  private void ShowLaser()
  {
    if(this._bullet.activeSelf == false)
        this._bullet.SetActive(true);
  }

  private void HideLaser()
  {
    if(this._bullet.activeSelf == true)
      this._bullet.SetActive(false);
  }
}
