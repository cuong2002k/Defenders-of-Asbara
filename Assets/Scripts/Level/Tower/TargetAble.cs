using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAble : MonoBehaviour
{


  [Header("Target value")]
  protected Transform _target;
  public Transform Target => this._target;
  protected float _targetRange;
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
  }

  private void Update() 
  {
    if (this._target == null)
    {
      FindTarget();
      return;
    }

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
      int indexRandom = Random.Range(0, collider.Length);
      _target = collider[indexRandom].transform;
    }
  }

  /// <summary>
  /// tracking enemy target 
  /// </summary>
  /// <returns>if not target in range and out range is false, other wise</returns>
  private void TrackTarget()
  {
    if (this._target == null) return;
    Vector3 current = this.transform.position;
    Vector3 target = this._target.transform.position;
    float distance = Vector3.Distance(current, target);

    if (distance > this._targetRange)
    {
      this._target = null;
    }
  }

  protected virtual void OnDrawGizmosSelected()
  {
    Gizmos.DrawWireSphere(this.transform.position, this._targetRange);
  }
}
