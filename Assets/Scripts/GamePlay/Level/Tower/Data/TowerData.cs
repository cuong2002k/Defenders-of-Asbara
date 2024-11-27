using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/TowerData", fileName = "TowerData")]
public class TowerData : ScriptableObject
{
    [Header("Prefabs spawn in level")]
    public TowerLevel TowerPrefabs;
    [Header("Show image button tower")]
    public int TowerCost;
    [Header("Cost to sell tower")]
    public int TowerCostSell;
    [Header("Range to target")]
    public float TargetRange;
    [Header("Number of attacks per second")]
    public int AttackPerSecond;
    [Header("Number of dame per second")]
    public float DamePerSecond;
    [Header("Rotation speed of the turret")]
    public float RotationSpeed;
    public int TargetNumber;
    [Header("What layer tower target from?")]
    public LayerMask TargetLayer;


}
