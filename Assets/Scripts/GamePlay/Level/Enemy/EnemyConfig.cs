using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Data/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    public string EnemyName;
    public string EnemyDescription;
    public GameObject _enemyPrefabs;
}
