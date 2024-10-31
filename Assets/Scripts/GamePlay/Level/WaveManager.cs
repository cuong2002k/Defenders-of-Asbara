using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    /// <summary>
    /// Save all wave in 
    /// </summary>
    [SerializeField] private List<Wave> _waves = new List<Wave>();

    /// <summary>
    /// index of waves
    /// </summary>
    [SerializeField]private int _currentIndex = 0;
    public int CurrentWave => _currentIndex + 1;

    public event Action WaveChange;
    public event Action WaveComplete;

    public int WaveTotal => _waves.Count;

    public void StartWave()
    {
        if(this._waves.Count > 0)
        {
            InitCurrentWave();
        }
        else{
            Debug.LogError("No waves on wave manager, call wave complete");
            this.SafelyWaveComplete();
        }
    }

    public void NextWave()
    {
        Wave wave = this._waves[this._currentIndex];
        wave.WaveComplete -= NextWave;
        if(_waves.Next(ref this._currentIndex))
        {
            InitCurrentWave();
        }
        else
        {
            SafelyWaveComplete();
        }
    }

    public void InitCurrentWave()
    {
        Wave wave = this._waves[this._currentIndex];
        wave.WaveComplete += NextWave;
        wave.InitWave();
        if(WaveChange != null)
        {
            WaveChange();
        }
    }

    public void SafelyWaveComplete()
    {
        WaveComplete?.Invoke();
    }


}
