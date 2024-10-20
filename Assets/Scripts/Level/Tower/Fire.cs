using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Fire : TowerBase
{
  [SerializeField] private VisualEffect _flameThrower;
  private bool _isAttack = false;
  protected override void Start()
  {
    base.Start();
    if(_flameThrower == null)
    {
      Common.Log("Flame Thower effect not found in {0}", this.gameObject);
    }
    this._flameThrower.Stop();
  }

  protected override void Update()
  {
    base.Update();
    if(this._flameThrower == null) return;
    if(this._targetAble.CheckTarget() != null)
    {
      if(_isAttack == true) return;
      _isAttack = true;
      this._flameThrower.Play();
    }
    else{
      if(_isAttack == false) return;
      _isAttack = false;
      this._flameThrower.Stop();
    }
  }

  protected override void Shoot()
  {
    for(int i = 0; i < this._target.Length ;i++)
    {
      if(_target[i] == null) continue;
      IDamage damage = this._target[i].GetComponent<IDamage>();
      if(damage != null)
      {
        damage.TakeDamage(1);
      }
    }
  }
}
