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
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    public Vector3 StartPoint => _startPoint.transform.position;
    public Vector3 EndPoint => _endPoint.transform.position;

    public GameObject EnemyContainer;

    [Header("Tower library")]
    [SerializeField] private TowerLibrary _towerLibrary;
    public TowerLibrary TowerLibrary => _towerLibrary;

    #region Unity Logic
    protected override void Awake()
    {
        base.Awake();
        _waveManager = GetComponent<WaveManager>();
        _waveManager.WaveComplete += OnSpawnComplete;
        this._levelState = LevelState.BUILDING;
    }

    private void Start() {
      // register listener event when click start wave
      this.RegisterListener(EventID.OnStartWave, (param) => OnSpawnEnemy());
    }
    #endregion

    #region Level State
    // Fiels
    [SerializeField]private LevelState _levelState;
    private int _enemyNumber = 0;
    // end Fiels
    private void ChangeLevelState(LevelState newState)
    {
        if(this._levelState == newState) return;
        _levelState = newState;
        switch(newState)
        {
          case LevelState.SPAWN_ENEMIES:
            _waveManager.StartWave();
            break;
          case LevelState.BUILDING:
            break;
          case LevelState.WIN:
            Debug.Log("Win");
            break;
          case LevelState.LOSE:
            Debug.Log("Lose");
            break;
          case LevelState.ALL_ENEMIES_SPAWN:
            if(_enemyNumber == 0)
            {
              Debug.Log("Level Complete");
            }
            break;
        }
    }

    private void OnSpawnComplete(){
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

      if(this._enemyNumber == 0 && this._levelState == LevelState.ALL_ENEMIES_SPAWN)
      {
        this.ChangeLevelState(LevelState.WIN);
      }
    }

    public void AddCoin()
    {
      
    }

    #endregion End LevelState

    public void UpdateEnemyPath()
    {
      for(int i = 0; i < EnemyContainer.transform.childCount; i++)
      {
        Enemy enemy = EnemyContainer.transform.GetChild(i).GetComponent<Enemy>();
        if(enemy != null)
        {
          enemy.SetPath(PathManager.Instance.Paths);
        }
      }
    }
    
}