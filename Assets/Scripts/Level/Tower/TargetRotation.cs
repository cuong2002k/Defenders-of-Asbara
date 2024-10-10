using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(TargetAble))]
[RequireComponent(typeof(TowerBase))]
public class TargetRotation : MonoBehaviour
{
    [Header("Component")]
    TargetAble _targetAble;
    TowerBase _towerBase;

    [Header("Turret")]
    [SerializeField] protected Transform _turret; // ref with turret of tower to rotation
    protected float _rotationSpeed; // speed rotation when have target

    private void Start() {
      if(_turret == null)
      {
        Common.Log("Turret tranform is not assign in object {0}", this.gameObject.name);
        Destroy(this.gameObject);
      }
      this._targetAble = GetComponent<TargetAble>();
      this._towerBase = GetComponent<TowerBase>();
      // set data form tower data
      this._rotationSpeed = this._towerBase.TowerData.RotationSpeed;
    }

    private void Update() {
      Transform target = this._targetAble.Target;
      if(target != null)
      {
        LookTarget(target);
      }
    }

    /// <summary>
    /// Rotation following enemy in range
    /// </summary>
    /// <param name="target"> the turret will rotate accordingly </param>
    private void LookTarget(Transform target)
    {
      Vector3 targetPosition = target.transform.position;
      Vector3 current = this._turret.transform.position;
      Vector3 direction = targetPosition - current;
      Quaternion lookRotation = Quaternion.LookRotation(direction);
      Vector3 rotation = Quaternion.Lerp(_turret.rotation, lookRotation, _rotationSpeed * Time.deltaTime).eulerAngles;
      _turret.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
}
