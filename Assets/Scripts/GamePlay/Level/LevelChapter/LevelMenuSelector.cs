using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMenuSelector : MonoBehaviour
{
    public static int _currentLevel = 0;
    public static int _totalStar = 0;

    [SerializeField] private LevelSelector[] _levelSelectors;

    private void Start()
    {
        for (int i = 0; i < _levelSelectors.Length; i++)
        {
            _levelSelectors[i].Initialize();
        }
        UnlockLevel();
    }

    [ContextMenu("Set Level")]
    private void UnlockLevel()
    {
        if (_currentLevel > _levelSelectors.Length) return;
        _currentLevel++;
        for (int i = 0; i < _levelSelectors.Length; i++)
        {
            if (i < _currentLevel)
            {
                _levelSelectors[i].Unlock();
                int starSave = PlayerPrefs.GetInt("Level" + (i + 1), 0);
                _levelSelectors[i].SetStar(starSave);
            }
        }
    }

    private void SetStar(int star)
    {
        _levelSelectors[_currentLevel].SetStar(star);
    }

}