using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/towerData", fileName = "TowerData")]
public class TowerData : ScriptableObject
{
   public string Name;
   public Sprite Icon;
   public float TargetRange;
   public float AttackPerSecond;
   public float DamePersecond;
   public float RotationSpeed;
   public LayerMask TargetLayer;

}
