using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(TargetAble))]
public abstract class TowerBase : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] protected TowerData _towerData;
    public TowerData TowerData => this._towerData;
    
    [Header("Reference")]
    [SerializeField] protected Transform[] _fireTranform;
    [SerializeField] protected GameObject _bullet;

    protected Transform[] _target;
    protected virtual void Start()
    {
      _targetAble = GetComponent<TargetAble>();
      this._attackPerSecond = this._towerData.AttackPerSecond;
    }

    protected virtual void Update()
    {
      Transform target = this._targetAble.CheckTarget();
      
      if(target == null) return;
      this. _target = this._targetAble.Target;
      CheckAttacking();
    }

    #region Attack
    protected float _attackPerSecond;
    protected TargetAble _targetAble;
    private float _currentTimer = 0;

    private void CheckAttacking()
    {
      if(this.CheckTimer())
      {
        this.Shoot();
      }
    }

    private bool CheckTimer()
    {
      _currentTimer += Time.deltaTime;
      if(_currentTimer >= 1f/this._attackPerSecond)
      {
        _currentTimer = 0;
        return true;
      }
      return false;
    }

    protected abstract void Shoot();

    protected virtual GameObject SpawnBullet(GameObject bulletObject, Vector3 position)
    {
      GameObject bullet = PoolManager.Instance.GetObjectPool(bulletObject);
      bullet.transform.position = position;
      return bullet;
    }

    #endregion
}
