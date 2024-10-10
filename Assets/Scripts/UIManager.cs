using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public WaveUI WaveUI { get; private set; }

    // Start is called before the first frame update
    protected override void Awake() {
      base.Awake();
      WaveUI = GetComponentInChildren<WaveUI>();
    }
}
