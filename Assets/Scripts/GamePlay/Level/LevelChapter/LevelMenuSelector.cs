using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMenuSelector : MonoBehaviour
{
    public static int _currentLevel = 0;
    public static int _totalStar = 0;

    [SerializeField] private LevelSelector[] _levelSelectors;

    public static int MaxLevel = 0;

    private void Start()
    {
        for (int i = 0; i < _levelSelectors.Length; i++)
        {
            _levelSelectors[i].Initialize();
            _levelSelectors[i].setIndex(i + 1);
        }
        UnlockLevel();
        MaxLevel = _levelSelectors.Length;
    }

    private void UnlockLevel()
    {
        _currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        for (int i = 0; i < _levelSelectors.Length; i++)
        {
            if (i < _currentLevel && i < _levelSelectors.Length)
            {
                _levelSelectors[i].Unlock();
                int starSave = PlayerPrefs.GetInt("Level" + (i + 1), 0);
                _levelSelectors[i].SetStar(starSave);
            }
        }
    }

}
