using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField]private LevelState _levelState;

    [SerializeField]private int _enemyNumber = 0;

    protected override void Awake()
    {
        base.Awake();
        _waveManager = GetComponent<WaveManager>();
        _waveManager.WaveComplete += OnSpawnComplete;
        this._levelState = LevelState.BUILDING;
    }

    private void Start()
    {
      
    }

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

    void Update()
    {
      if(Input.GetKeyDown(KeyCode.Y))
      {
        ChangeLevelState(LevelState.SPAWN_ENEMIES);
      }
    }

    private void OnSpawnComplete(){
      ChangeLevelState(LevelState.ALL_ENEMIES_SPAWN);
    }

    public void AddEnemyNumber()
    {
      _enemyNumber++;
    }

    public void MinusEnemyNumber()
    {
      _enemyNumber--;
    }

    
}