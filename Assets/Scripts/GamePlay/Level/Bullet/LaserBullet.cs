using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LaserBullet : BulletBase
{
    private LineRenderer _lineRenderer;
    private Transform _beginTranform = default;
    private int _numberAttack;
    protected override void Start()
    {
        base.Start();
        this._lineRenderer = GetComponent<LineRenderer>();
        // Attack();
    }

    public void Attack()
    {

        if (_lineRenderer == null)
        {
            this._lineRenderer = GetComponent<LineRenderer>();
        }
        this._lineRenderer.positionCount = this._targets.Count + 1;

        this._lineRenderer.SetPosition(0, _beginTranform.position);

        for (int i = 0; i < this._targets.Count; i++)
        {
            if (this._targets[i] == null) continue;
            Enemy enemy = _targets[i].GetComponent<Enemy>();

            if (enemy != null)
            {
                this._lineRenderer.SetPosition(i + 1, enemy.GetTargetPos);
            }
        }

        for (int i = 0; i < this._targets.Count && i < this._numberAttack + 1; i++)
        {
            if (_targets[i] == null) continue;
            IDamage damage = this._targets[i].GetComponent<IDamage>();

            if (damage != null)
            {
                damage.TakeDamage(_damage);
            }
        }
    }

    public void Initialize(Transform begin, int numberAttack)
    {
        _beginTranform = begin;
        _numberAttack = numberAttack;
        Attack();

    }
}
