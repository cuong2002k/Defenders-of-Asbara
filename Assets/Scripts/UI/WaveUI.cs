using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WaveUI : MonoBehaviour
{
    public TextMeshProUGUI WaveInfor;
    private LevelManager _levelManager;

    private const string PREFIX_WAVE_INFO = "WAVE ";
    private const string MIDLE_WAVE_INFOR = "/";

    void Start()
    {
      CacheComponent();
      Init();
    }

    private void Update()
    {
      WaveInfor.text = PREFIX_WAVE_INFO +  _levelManager.WaveManager.CurrentWave + MIDLE_WAVE_INFOR + _levelManager.WaveManager.WaveTotal;
    }

    private void Init()
    {
      WaveInfor.text = PREFIX_WAVE_INFO +  _levelManager.WaveManager.CurrentWave + MIDLE_WAVE_INFOR + _levelManager.WaveManager.WaveTotal;
      this.gameObject.SetActive(false);
    }

    private void CacheComponent()
    {
      WaveInfor = GetComponentInChildren<TextMeshProUGUI>();
      _levelManager = LevelManager.Instance;
    }
}
