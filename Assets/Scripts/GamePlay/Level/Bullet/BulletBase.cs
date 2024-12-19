using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : PoolAble
{
    [Header("REFERENTS")]
    [SerializeField] protected Transform _target;
    [SerializeField] protected GameObject _hitEffect;
    protected Rigidbody _rigidbody;

    [Header("MOVEMENT")]
    [SerializeField] protected float _speed = 7f;

    [Header("ATTACK")]
    protected int _damage = 2;

    protected virtual void Start()
    {
        InitCompnent();

    }

    public virtual void Initialized(int damage)
    {
        this._damage = damage;
    }


    public virtual void Initialized(Transform target, int damage)
    {
        this._target = target;
        this._damage = damage;
    }

    protected void InitCompnent()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
}
