using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
[RequireComponent(typeof(WaveManager))]
public class LevelManager : Singleton<LevelManager>
{

    [Header("Wave infor")]
    private WaveManager _waveManager;

    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    public Vector3 StartPoint => _startPoint.transform.position;
    public Vector3 EndPoint => _endPoint.transform.position;
    [SerializeField] private Vector3[] _path;

    private LevelState _levelState;
    protected override void Awake()
    {
        base.Awake();
        _waveManager = GetComponent<WaveManager>();
        _waveManager.WaveComplete += OnSpawnComplete;

        this._levelState = LevelState.BUILDING;
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
            break;
          case LevelState.LOSE:
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

    }

    
}