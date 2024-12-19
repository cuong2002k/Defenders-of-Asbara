using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPanel : ViewBase
{
    private int _kill = 0;
    private int _star = 0;
    [SerializeField] private Star[] _stars;

    [SerializeField] private Button _back;
    [SerializeField] private Button _reset;
    [SerializeField] private Button _next;


    private void Start()
    {
        this.Initialize();
    }

    public override void Initialize()
    {
        this.Hide();
        _back.onClick.AddListener(this.BackMainMenu);
    }

    public void Initialize(int Health, int maxHealth, int kill)
    {
        this._kill = kill;
        if (Health >= maxHealth - maxHealth * 0.2f)
        {
            _star = 3;
        }
        else if (Health >= maxHealth - maxHealth * 0.5f)
        {
            _star = 2;
        }
        else
        {
            _star = 1;
        }

        for (int i = 0; i < _stars.Length; i++)
        {
            if (i < _star)
            {
                _stars[i].Show();
            }
        }

        PlayerPrefs.SetInt("Level1", _star);
    }

    private void BackMainMenu()
    {
        Loader.LoadScene(SceneName.Map);
    }

    private void Reset()
    {
        Loader.LoadScene(SceneName.Level1);
    }
}
