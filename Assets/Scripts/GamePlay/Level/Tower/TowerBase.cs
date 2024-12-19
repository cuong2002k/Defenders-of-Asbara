using System.Collections;
using System.Collections.Generic;
using DefenderOfAsbara.UI;
using UnityEngine;
public abstract class TowerBase : PoolAble
{
    /// <summary>
    /// Target Rotation Component
    /// </summary>
    protected TargetRotation _targetRotation;

    [Header("Tower data Config")]
    [SerializeField] protected TowerConfig _towerConfig;
    public TowerConfig TowerConfig => _towerConfig;

    [Header("Ref with desier bullet type")]
    [SerializeField] protected GameObject _bulletPrefabs;
    [SerializeField] protected GameObject _muzzle;

    [Header("Ref with attack tranform of tower")]
    [SerializeField] protected Transform[] _attackTranform;

    [Header("Save all tower level")]
    [SerializeField] protected TowerLevel[] _towerLevels;
    [Header("Current level index")]
    protected int _currentLevel = 0; // index level
    protected TowerLevel _currentTowerLevel; // save current level of tower

    #region Unity Logic
    protected virtual void Awake()
    {
        CacheComponent();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        Transform target = this._targetter.GetFirstTarget();

        if (target == null) return;
        CheckAttacking();
    }

    #endregion End Unity Logic

    protected virtual void CacheComponent()
    {
        _targetter = GetComponentInChildren<Targetter>();
        _targetRotation = GetComponent<TargetRotation>();

    }

    [ContextMenu("Init")]
    protected virtual void Initialized()
    {
        TowerData towerData = CurrentTowerLevel.TowerLevelData;
        _currentTowerLevel = Instantiate(towerData.TowerPrefabs.gameObject, this.transform).GetComponent<TowerLevel>();
        this._attackPerSecond = towerData.TryGetStats(StatsType.ATTACKRATE);
        _currentTimer = 1f / this._attackPerSecond;

        if (_targetRotation != null)
        {
            _targetRotation.Initialize(towerData.RotationSpeed, this._currentTowerLevel.Turret);
        }

        // setup attack tranform
        if (_currentTowerLevel != null)
        {
            _attackTranform = _currentTowerLevel.AttackTranform;
        }

        // Setup targeter
        if (_targetter != null)
        {
            _targetter.Initialize(towerData.TryGetStats(StatsType.ATTACKRANGE), TowerConfig.TargetLayer);
        }

    }

    #region Attack
    protected float _attackPerSecond;
    protected Targetter _targetter;
    private float _currentTimer = 0;

    protected virtual void CheckAttacking()
    {
        if (this.CheckTimer())
        {
            this.Shoot();
        }
    }

    protected virtual bool CheckTimer()
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

    protected virtual GameObject SpawnPrefabs(GameObject prefabs, Vector3 position)
    {
        GameObject bullet = PoolAble.TryGetPool(prefabs);
        bullet.transform.position = position;
        return bullet;
    }

    protected int finalDamage
    {
        get
        {
            TowerData data = this._currentTowerLevel.TowerLevelData;
            float dameRandom = Random.Range(data.TryGetStats(StatsType.MINDAMAGE), data.TryGetStats(StatsType.MAXDAMAGE));
            return Mathf.RoundToInt(dameRandom);
        }

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
        Initialized();
    }

    public void SellTower()
    {
        LevelManager.Instance.AddCoin(this.CurrentTowerLevel.GetTowerCostSell);

        TowerLevel towerLevel = GetComponentInChildren<TowerLevel>();

        if (towerLevel != null)
        {
            this.OnDespawn();
            Destroy(towerLevel.gameObject);
        }
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
        Initialized();
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        this._currentLevel = 0;
        Destroy(this._currentTowerLevel.gameObject);
        PoolAble.TryReturn(this.gameObject);
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
            return this._towerLevels[this._currentLevel].TowerLevelData.TryGetStats(StatsType.ATTACKRANGE);
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

    #region tranform
    protected virtual Transform GetFirstAttackPoint()
    {
        if (this._attackTranform.Length == 0) return this.transform;
        return this._attackTranform[0];
    }

    protected virtual Transform GetRandomAttackPoint()
    {
        if (this._attackTranform.Length == 0) return this.transform;
        int randomNum = Random.Range(0, this._attackTranform.Length);
        return this._attackTranform[randomNum];
    }

    protected virtual Transform GetAttackPointInOrder(ref int index)
    {
        if (this._attackTranform.Length == 0) return this.transform;
        if (index >= this._attackTranform.Length)
        {
            index = -1;
        }
        index++;
        return _attackTranform[index];

    }

    #endregion
}
