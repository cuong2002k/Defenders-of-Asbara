using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : TimerBehavior
{
    [Header("Base value")]
    [SerializeField] private float _targetRange;
    [SerializeField] private LayerMask _layerTarget;
    [SerializeField] private Transform _turret;

    [Header("Target value")]
    private Target _target;
    private float _rotationSpeed = 10f;
    private float _firePerSecond = 2f;
    [SerializeField]private RepeatTimer repeatTimer;
    [SerializeField]private GameObject _bullet;
    [SerializeField] private Transform _fireTranform;

    void Start()
    {
      repeatTimer = new RepeatTimer(1f/_firePerSecond, Shoot);
      
    }

    protected override void Update()
    {
      base.Update();
        if(this._target == null)
        {
          FindTarget();
          return;
        }

        if(TrackTarget())
        {
           LookTarget();
           
        }
    }

    private void Shoot()
    {
      GameObject bullet = Instantiate(this._bullet, _fireTranform.position, _fireTranform.transform.rotation);
      bullet.GetComponent<Bullet>().SetTarget(this._target.transform);
    
    }

    private void FindTarget()
    {
      Collider[] collider = Physics.OverlapSphere(this.transform.position, this._targetRange, _layerTarget);
      if(collider.Length > 0)
      {
        this.StartTimer(repeatTimer);
        _target = collider[0].GetComponent<Target>();
        // return true;
      }

      // return false;
    }

    /// <summary>
    /// tracking enemy target 
    /// </summary>
    /// <returns>if not target in range and out range is false, other wise</returns>
    private bool TrackTarget(){
      if(this._target == null) return false;
      Vector3 current = this.transform.position;
      Vector3 target = this._target.transform.position;
      float distance = Vector3.Distance(current, target);

      if(distance > this._targetRange)
      {
        this.PauseTimer(repeatTimer);
        this._target = null;
        return false;
      }

      return true;
    }

    private void LookTarget()
    {
      Vector3 targetPosition = this._target.transform.position;
      Vector3 current = this._turret.transform.position;
      Vector3 direction = targetPosition - current;
      Quaternion lookRotation = Quaternion.LookRotation(direction);
      Vector3 rotation = Quaternion.Lerp(_turret.rotation, lookRotation, _rotationSpeed * Time.deltaTime).eulerAngles; 
      _turret.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void OnDrawGizmosSelected()
    {
      Gizmos.DrawWireSphere(this.transform.position, this._targetRange);
    }
}
