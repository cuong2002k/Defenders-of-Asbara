using System.Collections;
using System.Collections.Generic;
using DefenderOfAsbara.UI;
using UnityEngine;
public abstract class TowerBase : MonoBehaviour
{
    /// <summary>
    /// Animation Component
    /// </summary>
    protected TowerAmationController _animationControl;
    /// <summary>
    /// Target Rotation Component
    /// </summary>
    protected TargetRotation _targetRotation;

    [Header("Tower data Config")]
    [SerializeField] protected TowerConfig _towerConfig;
    public TowerConfig TowerConfig => _towerConfig;

    [Header("Ref with desier bullet type")]
    [SerializeField] protected GameObject _bullet;

    [Header("Ref with attack tranform of tower")]
    [SerializeField] protected Transform[] _attackTranform;

    [Header("Save all tower level")]
    [SerializeField] protected TowerLevel[] _towerLevels;
    [Header("Current level index")]
    protected int _currentLevel = 0;
    public TowerLevel _currentTowerLevel;

    #region Unity Logic
    protected virtual void Awake()
    {
        CacheComponent();
    }

    protected virtual void Start()
    {
        Initialized(this.CurrentTowerLevel);
    }

    protected virtual void Update()
    {
        Transform target = this._targetter.GetFirstTarget();

        if (target == null)
        {
            this._animationControl?.ResetAnimation();
            return;
        }

        CheckAttacking();
    }

    #endregion End Unity Logic

    private void CacheComponent()
    {
        _targetter = GetComponentInChildren<Targetter>();
        _targetRotation = GetComponent<TargetRotation>();
    }

    private void Initialized(TowerLevel towerLevel)
    {
        TowerData towerData = towerLevel.TowerLevelData;
        _currentTowerLevel = Instantiate(towerData.TowerPrefabs.gameObject, this.transform).GetComponent<TowerLevel>();
        if (_currentTowerLevel != null)
        {
            _targetter.Initialize(towerData.TargetRange, towerData.TargetLayer);
            _animationControl = GetComponentInChildren<TowerAmationController>();
            _animationControl.SetAnimationSpeed(towerData.AttackPerSecond);
            _attackTranform = _currentTowerLevel.AttackTranform;
            this._attackPerSecond = towerData.AttackPerSecond;
            this._targetRotation.Initialize(towerData.RotationSpeed, _currentTowerLevel.Turret);
            _currentTimer = 1f / this._attackPerSecond;
        }
    }

    #region Attack
    protected float _attackPerSecond;
    protected Targetter _targetter;
    public Targetter Targetter => _targetter;
    private float _currentTimer = 0;

    private void CheckAttacking()
    {
        if (this.CheckTimer())
        {
            this.Shoot();
        }
    }

    private bool CheckTimer()
    {
        _currentTimer += Time.deltaTime;
        if (_currentTimer >= 1f / this._attackPerSecond)
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

    #endregion End Attack

    #region Upgrade or Sell tower
    public bool IsMaxLevel
    {
        get
        {
            return (this._currentLevel + 1) >= this._towerLevels.Length;
        }
    }

    public int GetNextCostLevel
    {
        get
        {
            if (this.IsMaxLevel)
            {
                return 0;
            }
            return this._towerLevels[this._currentLevel + 1].GetTowerCost;
        }
    }

    public int GetCurrentCostLevel
    {
        get
        {
            return this._towerLevels[this._currentLevel].GetTowerCost;
        }
    }

    public int GetCurrentSellCostLevel
    {
        get
        {
            if (this.CurrentTowerLevel == null)
            {
                Common.LogWarning("current Tower data is null {0}", this.gameObject);
                return 0;
            }
            return this.CurrentTowerLevel.GetTowerCostSell;
        }
    }

    public void UpgradeTower()
    {
        if (this.IsMaxLevel)
        {
            Common.LogWarning("Current Tower level is max, cannot upgrade them {0}", this.gameObject);
            return;
        }
        Destroy(_currentTowerLevel.gameObject);
        this._currentLevel++;
        LevelManager.Instance.MinusCoin(this.GetCurrentCostLevel);
        Initialized(this.CurrentTowerLevel);
    }

    public void SellTower()
    {
        LevelManager.Instance.AddCoin(this.CurrentTowerLevel.GetTowerCostSell);

        TowerLevel towerLevel = GetComponentInChildren<TowerLevel>();

        if (towerLevel != null)
        {
            Destroy(towerLevel.gameObject);
            this.gameObject.SetActive(false);
        }
    }

    public float CurrentTargetRange
    {
        get
        {
            if (this._currentLevel > this._towerLevels.Length)
            {
                Common.LogWarning("Out range in tower level", this.gameObject);
                return 0;
            }
            return this._towerLevels[this._currentLevel].TowerLevelData.TargetRange;
        }
    }

    public TowerLevel CurrentTowerLevel
    {
        get
        {
            if (this._currentLevel > this._towerLevels.Length)
            {
                Common.LogWarning("Out range in tower level", this.gameObject);
                return null;
            }

            return this._towerLevels[this._currentLevel];
        }
    }

    #endregion
    public Color GetTargetRangeColor
    {
        get
        {
            if (this._towerConfig == null)
            {
                Common.LogWarning("TowerConfig is null {0}", this.gameObject);
                return Color.blue;
            }
            return this._towerConfig.color;
        }
    }
}
