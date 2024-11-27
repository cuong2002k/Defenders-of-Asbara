using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Laser : TowerBase
{
    LineRenderer _lineRenderer;

    [SerializeField] private int _numberAttack = 3;

    protected override void Start()
    {
        base.Start();
        this._lineRenderer = this._bullet.GetComponent<LineRenderer>();
        this._lineRenderer.positionCount = 2;
    }

    protected override void Shoot()
    {
        this._animationControl.PlayAttackAnimation();
        List<Transform> targets = this._targetter.GetAllTarget();
        LaserBullet laserBullet = PoolManager.Instance.GetObjectPool(_bullet).GetComponent<LaserBullet>();
        laserBullet.SetTarget(targets);
        laserBullet.Initialize(this._attackTranform[0], this._numberAttack);
    }
}
