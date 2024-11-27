using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : ViewBase
{
    [SerializeField] private Button _startButton;
    public override void Initialize()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }
}
