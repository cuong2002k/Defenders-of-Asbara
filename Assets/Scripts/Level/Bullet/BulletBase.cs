using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
  protected Transform _target;
  [SerializeField] 
  protected float _speed = 7f;
  protected Rigidbody _rigidbody;
  protected int _damage = 2;

  protected virtual void Start()
  {
    _rigidbody = GetComponent<Rigidbody>();

  }

  public void SetTarget(Transform target)
  {
    this._target = target;
  }

}
