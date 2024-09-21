using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WaveUI : MonoBehaviour
{
    public TextMeshProUGUI WaveInfor;
    
    private LevelManager _levelManager;
    void Start()
    {
       _levelManager = LevelManager.Instance;
       if(_levelManager == null) return;

       WaveInfor.text = "WAVE " +  _levelManager.WaveManager.CurrentWave + "/" + _levelManager.WaveManager.WaveTotal;
    }

    private void Update()
    {
      WaveInfor.text = "WAVE " +  _levelManager.WaveManager.CurrentWave + "/" + _levelManager.WaveManager.WaveTotal;
    }
}
