using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLevel : MonoBehaviour
{
    [SerializeField] private TowerData _towerData;

    public TowerData TowerLevelData => _towerData;

    [SerializeField] private Transform _turret;

    public Transform Turret => this._turret;

    [SerializeField] private Transform[] _attackTranform;

    public Transform[] AttackTranform => _attackTranform;


    public int GetTowerCost
    {
        get
        {
            return Mathf.RoundToInt(this._towerData.TryGetStats(StatsType.COST));
        }
    }

    public int GetTowerCostSell
    {
        get
        {
            return Mathf.RoundToInt(this._towerData.TryGetStats(StatsType.COSTSELL));
        }
    }

}
