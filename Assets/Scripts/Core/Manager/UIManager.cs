using System.Collections;
using System.Collections.Generic;
using DefenderOfAsbara.UI;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public WaveUI WaveUI { get; private set; }
    public TowerUI TowerUI { get; private set; }

    [SerializeField] private List<ViewBase> viewBases = new List<ViewBase>();
    private Stack<ViewBase> _history = new Stack<ViewBase>();
    private ViewBase _currentView;

    protected override void Awake()
    {
        base.Awake();
        WaveUI = GetComponentInChildren<WaveUI>();
        TowerUI = GetComponentInChildren<TowerUI>();
    }

    public static ViewBase GetView<T>() where T : ViewBase
    {
        for (int i = 0; i < _instance.viewBases.Count; i++)
        {
            if (_instance.viewBases[i] is T tView)
                return tView;
        }
        return null;
    }

    public static void Show<T>(bool remember = true) where T : ViewBase
    {
        for (int i = 0; i < _instance.viewBases.Count; i++)
        {
            if (_instance.viewBases[i] is T)
            {
                if (_instance._currentView == null) return;
                if (remember)
                {
                    _instance._history.Push(_instance.viewBases[i]);
                }
                _instance._currentView.Hide();
                _instance._currentView = _instance.viewBases[i];
            }
        }
    }

    public static void Show(ViewBase view, bool remember = true)
    {
        if (_instance._currentView != null)
        {
            if (remember)
            {
                _instance._history.Push(_instance._currentView);
            }
            _instance._currentView.Hide();
        }

        view.Show();
        _instance._currentView = view;
    }

    public static void ShowLast()
    {
        if (_instance._history.Count != 0)
        {
            Show(_instance._history.Pop(), false);
        }
    }


}
