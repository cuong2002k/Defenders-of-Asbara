using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetter : MonoBehaviour
{
    [Header("Target value")]
    [SerializeField] protected List<Transform> _enemyInRanges = new List<Transform>();

    [Header("Distance tower can attack")]
    protected float _targetRange;
    public float TargetRange => TargetRange;

    [SerializeField] protected Color _colorEffect;
    public Color ColorEffect => _colorEffect;

    [Header("Layer check target")]
    protected LayerMask _layerTarget;

    private SphereCollider _collider;

    #region Unity Logic
    private void Awake()
    {
        this._collider = GetComponent<SphereCollider>();
    }

    /// <summary>
    /// Initial targetter
    /// </summary>
    /// <param name="range">radius target</param>
    /// <param name="targetLayer">what layer target</param>
    public void Initialize(float range, LayerMask targetLayer)
    {
        this._targetRange = range;
        this._layerTarget = targetLayer;
        this._collider.radius = this._targetRange;
    }

    /// <summary>
    /// Get target when in range
    /// </summary>
    /// <param name="other">target</param>
    private void OnTriggerEnter(Collider other)
    {
        IDamage damage = other.GetComponent<IDamage>();
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.OnDie += this.OnRemoveTarget;
        }
        if (damage != null && !this._enemyInRanges.Contains(other.transform))
        {
            this._enemyInRanges.Add(other.transform);
        }
    }

    /// <summary>
    /// Remove target out range
    /// </summary>
    /// <param name="other">target is out range</param>
    private void OnTriggerExit(Collider other)
    {
        OnRemoveTarget(other.transform);
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.OnDie -= this.OnRemoveTarget;
        }
    }

    #endregion

    /// <summary>
    /// Get First Enemy in Range
    /// </summary>
    /// <returns></returns>
    public Transform GetFirstTarget()
    {
        for (int i = 0; i < this._enemyInRanges.Count; i++)
        {
            if (this._enemyInRanges[i] != null)
                return this._enemyInRanges[i];
        }

        return null;
    }

    /// <summary>
    /// Get All target in range
    /// </summary>
    /// <returns></returns>
    public List<Transform> GetAllTarget()
    {
        return this._enemyInRanges;
    }

    public Transform GetRandomTarget()
    {
        int randomNum = Random.Range(0, this._enemyInRanges.Count);
        return this._enemyInRanges[randomNum];
    }

    private void OnRemoveTarget(Transform target)
    {
        if (this._enemyInRanges.Contains(target))
        {
            this._enemyInRanges.Remove(target);

        }
    }
}
