using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/TowerData", fileName = "TowerData")]
public class TowerData : ScriptableObject
{
  [Header("Name of Tower")]
   public string Name;
  [Header("Ghost tower check valid and invalid")]
   public GhostTower GhostTower;
   [Header("Prefabs spawn in level")]
   public TowerBase TowerPrefabs;
   [Header("Show image button tower")]
   public int TowerCost;
   public Sprite Icon;
   public float TargetRange;
   public float AttackPerSecond;
   public float DamePersecond;
   public float RotationSpeed;
   public int TargetNumber;
   public LayerMask TargetLayer;

}
