using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : ViewBase
{
    private Button _buttonSelectLevel;
    [SerializeField] private SceneName _sceneName;
    [SerializeField] private Star[] _stars;
    [SerializeField] private bool _unlock = false;
    [SerializeField] private int _star = 0;

    private void Awake()
    {
        _buttonSelectLevel = GetComponent<Button>();

    }

    private void Start()
    {
        _buttonSelectLevel.onClick.AddListener(SelectLevel);
    }

    private void SelectLevel()
    {
        Loader.LoadScene(this._sceneName);
    }

    public void SetStar(int star)
    {
        this._star = star;
        for (int i = 0; i < this._stars.Length; i++)
        {
            if (i < this._star)
            {
                this._stars[i].Show();
            }
        }
    }

    public void Unlock()
    {
        _unlock = true;
        this._buttonSelectLevel.interactable = _unlock;
    }

    private void OnDisable()
    {
        _buttonSelectLevel.onClick.RemoveListener(SelectLevel);
    }

    public override void Initialize()
    {
        _buttonSelectLevel.interactable = false;
    }
}
