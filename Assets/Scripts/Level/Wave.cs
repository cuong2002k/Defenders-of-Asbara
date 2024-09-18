using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : TimerBehavior
{
    
    // list monsters will appear in the wave 
    [SerializeField]private List<SpawnIntruction> _waveIntruction;
    // the index current wave spawn
    private int _currentWaveIndex = 0;

    /// <summary>
    /// repeat spawn enemy
    /// </summary>
    private RepeatTimer _repeatTimer;

    /// <summary>
    /// fire event when wave complete
    /// </summary>
    public event Action WaveComplete;


    public void InitWave()
    {
        if(this._waveIntruction.Count == 0)
        {
            Debug.LogError("====== THIS WAVE IS EMPTY =====");
            SafeCompleteEvent();
            return;
        }
        this._repeatTimer = new RepeatTimer(this._waveIntruction[_currentWaveIndex].DelayToSpawn, SpawnCurrent);
        this.StartTimer(_repeatTimer);
    }

    private void SpawnCurrent()
    {
        Spawn();
        if(!TrySetupNextWave())
        {
            this.SafeCompleteEvent();

            //
        }
    }   

    private void Spawn()
    {
        SpawnIntruction intruction = this._waveIntruction[_currentWaveIndex];
        Vector3 pos = new Vector3(3.99f, 0, 0.98f);
        SpawnIntruction(intruction.EnemyPrefabs, pos);
    }

    private bool TrySetupNextWave()
    {
        bool hasNext = this._waveIntruction.Next(ref this._currentWaveIndex);
        if(hasNext)
        {
            SpawnIntruction nextSpawnIntruction = this._waveIntruction[this._currentWaveIndex];
            if(nextSpawnIntruction.DelayToSpawn <= 0)
            {
                SpawnCurrent();
            }
            else{
                this._repeatTimer.SetTimer(nextSpawnIntruction.DelayToSpawn);
            }
        }
        return hasNext;
    }

    private void SafeCompleteEvent()
    {
        this.WaveComplete?.Invoke();
    }

    private void SpawnIntruction(EnemyConfig enemyConfig, Vector3 pos) //, Node startNode
    {
        Vector3 startPosition =pos ;
        Enemy enemy = Instantiate(enemyConfig._enemyPrefabs).GetComponent<Enemy>();
        enemy.SetPath(PathManager.Instance.Paths);
        enemy.transform.position = startPosition;

      
    }

}
