using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAble : MonoBehaviour
{


  [Header("Target value")]
  protected Transform[] _target = new Transform[20];
  public Transform[] Target => this._target;
  protected float _targetRange;
  protected int _targetNumber;
  protected LayerMask _layerTarget;

  private void Start() {

    TowerBase towerBase = GetComponent<TowerBase>();
    if(towerBase == null)
    {
      Common.Log("Cannot find Towerbase Script {0}", this.gameObject.name);
      return;
    }
    if(towerBase.TowerData == null)
    {
      Common.Log("Cannot find dataTower in Towerbase Script {0}", this.gameObject.name);
      return;
    }
    this._targetRange = towerBase.TowerData.TargetRange;
    this._layerTarget = towerBase.TowerData.TargetLayer;
    this._targetNumber = towerBase.TowerData.TargetNumber;
    this._target = new Transform[this._targetNumber];
  }

  private void Update() 
  {
    FindTarget();
    TrackTarget();
  }
  
  /// <summary>
  /// Find all object in range target
  /// </summary>
  protected virtual void FindTarget()
  {
    Collider[] collider = Physics.OverlapSphere(this.transform.position, this._targetRange, _layerTarget);
    if (collider.Length > 0)
    {
      for(int i = 0; i < collider.Length && i < this._targetNumber; i++)
      {
        _target[i] = collider[i].transform;
      }
    }
  }

  /// <summary>
  /// tracking enemy target 
  /// </summary>
  /// <returns>if not target in range and out range is false, other wise</returns>
  private void TrackTarget()
  {    
    for(int i = 0; i < this._target.Length; i++)
    {
      if(this._target[i] == null) return;
      Vector3 current = this.transform.position;
      Vector3 target = this._target[i].position;
      float distance = Vector3.Distance(current, target);
      if (distance > this._targetRange)
      {
        this._target[i] = null;
      }
    }
  }

  public Transform CheckTarget()
  {
    for (int i = 0; i < this._targetNumber; i++)
    {
      if (_target[i] != null)
      {
        return this._target[i];
      }
    }

    return null;
  }

  protected virtual void OnDrawGizmosSelected()
  {
    Gizmos.DrawWireSphere(this.transform.position, this._targetRange);
  }
}
