using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    protected Transform _target;
    [SerializeField] protected List<Transform> _targets = new List<Transform>();
    [SerializeField]
    protected float _speed = 7f;
    [SerializeField] protected Rigidbody _rigidbody;
    protected int _damage = 2;

    protected virtual void Start()
    {
        InitCompnent();

    }

    public virtual void SetTarget(Transform target)
    {
        this._target = target;
    }

    public virtual void SetTarget(List<Transform> targets)
    {
        this._targets = targets;
    }

    protected void InitCompnent()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

}
