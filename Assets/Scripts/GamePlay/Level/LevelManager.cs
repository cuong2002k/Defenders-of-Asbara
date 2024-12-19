using System;
using System.Collections;
using System.Collections.Generic;
using ObserverExtentision;
using UnityEngine;
[RequireComponent(typeof(WaveManager))]
public class LevelManager : Singleton<LevelManager>
{

    [Header("Wave infor")]
    private WaveManager _waveManager;
    public WaveManager WaveManager => _waveManager;
    [Header("Point spawn start")]
    [SerializeField] private Transform _startPoint;
    [Header("Point spawn end")]
    [SerializeField] private Transform _endPoint;
    public Vector3 StartPoint => _startPoint.transform.position;
    public Vector3 EndPoint => _endPoint.transform.position;

    [Header("Tower library")]
    [Tooltip("Save all tower allow building in level")]
    [SerializeField] private TowerLibrary _towerLibrary;
    public TowerLibrary TowerLibrary => _towerLibrary;

    [Tooltip("Coin using in level")]
    [SerializeField] private int _coinLevel;
    public int CoinLevel => this._coinLevel;

    [SerializeField] private int _health;
    public int Health => _health;

    [SerializeField] private int _maxHealth;
    public int MaxHealth => _maxHealth;

    private int _Kill = 0;

    #region Unity Logic
    protected override void Awake()
    {
        base.Awake();
        _waveManager = GetComponent<WaveManager>();
        _waveManager.WaveComplete += OnSpawnComplete;

    }

    private void Start()
    {
        // register listener event when click start wave
        this.RegisterListener(EventID.OnStartWave, (param) => OnSpawnEnemy());
        this._health = this._maxHealth;
    }

    private void OnDisable()
    {
        this.RemoveListener(EventID.OnStartWave, (param) => OnSpawnEnemy());
    }
    #endregion

    #region Level State
    // Fiels
    [SerializeField] private LevelState _levelState;
    [SerializeField] private int _enemyNumber = 0;
    // end Fiels
    public void ChangeLevelState(LevelState newState)
    {
        if (this._levelState == newState) return;
        _levelState = newState;
        switch (newState)
        {
            case LevelState.SPAWN_ENEMIES:
                _waveManager.StartWave();
                break;
            case LevelState.BUILDING:
                break;
            case LevelState.WIN:
                // open win panel
                WinPanel winPanel = UIManager.GetView<WinPanel>() as WinPanel;
                if (winPanel != null)
                {
                    winPanel.Initialize(_health, _maxHealth, this._Kill);
                    winPanel.Show();
                }
                GameManager.Instance.ChangeState(GameState.PAUSE);
                break;
            case LevelState.LOSE:
                Debug.Log("Lose");
                break;
            case LevelState.ALL_ENEMIES_SPAWN:
                if (_enemyNumber == 0)
                {
                    Debug.Log("Level Complete");
                }
                break;
        }
    }

    private void OnSpawnComplete()
    {
        ChangeLevelState(LevelState.ALL_ENEMIES_SPAWN);
    }

    public void OnSpawnEnemy()
    {
        ChangeLevelState(LevelState.SPAWN_ENEMIES);
    }

    public void AddEnemyNumber()
    {
        _enemyNumber++;
    }

    public void MinusEnemyNumber()
    {
        _enemyNumber--;

        if (this._enemyNumber <= 0 && this._levelState == LevelState.ALL_ENEMIES_SPAWN)
        {
            this.ChangeLevelState(LevelState.WIN);
        }
    }

    #endregion End LevelState

    #region Coin
    public void AddCoin(int coinReceiver)
    {
        this._coinLevel += coinReceiver;
        this.PostEvent(EventID.OnChangeCoin, this._coinLevel);
    }

    public void MinusCoin(int coinMinus)
    {
        this._coinLevel -= coinMinus;
        this.PostEvent(EventID.OnChangeCoin, this._coinLevel);
    }
    #endregion

    #region Health
    public void AttackHomeBase(int damage)
    {
        this._health -= damage;
        if (this._health <= 0)
        {
            this.ChangeLevelState(LevelState.LOSE);
        }
    }
    #endregion
}