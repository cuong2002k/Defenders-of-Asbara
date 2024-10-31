using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
  protected Transform _target;
  [SerializeField] 
  protected float _speed = 7f;
  [SerializeField]protected Rigidbody _rigidbody;
  protected int _damage = 2;

  protected virtual void Start()
  {
    InitCompnent();

  }

  public virtual void SetTarget(Transform target)
  {
    this._target = target;
  }

  protected void InitCompnent()
  {
    _rigidbody = GetComponent<Rigidbody>();
  }

}
