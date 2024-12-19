using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/TowerData", fileName = "TowerData")]
public class TowerData : ScriptableObject
{
    [Header("Prefabs spawn in level")]
    public TowerLevel TowerPrefabs;

    public List<StatsModifier> _statsModifiers = new List<StatsModifier>();

    public float RotationSpeed;

    public float TryGetStats(StatsType statsType)
    {
        for (int i = 0; i < this._statsModifiers.Count; i++)
        {
            if (_statsModifiers[i].StatusType == statsType)
            {
                return _statsModifiers[i].value;
            }
        }
        return 0;
    }
}
