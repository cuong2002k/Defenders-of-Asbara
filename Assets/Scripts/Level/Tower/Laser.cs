using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class Laser : TowerBase
{
  LineRenderer _lineRenderer;

  protected override void Start() {
    base.Start();
    _lineRenderer.GetComponent<LineRenderer>();
    _lineRenderer.positionCount = 2;
  }

  protected override void Shoot()
  {
    
    
  }

  protected override void Update()
  {
    base.Update();

    if(_target != null)
    {
      this._lineRenderer.gameObject.SetActive(true);
      Vector3 current = _fireTranform.position;
      Vector3 target = _target.transform.position;
      _lineRenderer.SetPosition(0, current);
      _lineRenderer.SetPosition(1, target);
    }
    else {
      this._lineRenderer.gameObject.SetActive(false);
    }
  }
}
