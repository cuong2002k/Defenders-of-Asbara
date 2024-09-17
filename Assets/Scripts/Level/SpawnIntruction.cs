using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnIntruction
{
    // Eneney Prefabs
    public EnemyConfig EnemyPrefabs;
    // time delay from previours spawn to current spawn
    public float DelayToSpawn;
    // point first spawn
    public Node StartNode;

}
