using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/TowerConfig", fileName = "TowerConfig")]
public class TowerConfig : ScriptableObject
{
    [Header("Name of Tower")]
    public string Name;
    [Header("Ghost tower check valid and invalid")]
    public GhostTower GhostTower;
    [Header("Prefabs spawn in level")]
    public TowerBase TowerPrefabs;
    [Header("Show image button tower")]
    public Sprite Icon;
    [Header("What color tower visualized")]
    public Color color;
    [Header("What layer tower target from?")]
    public LayerMask TargetLayer;
}
