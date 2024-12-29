using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyData", menuName = "Data/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Tooltip("Health of enemy")]
    public int Health;
    [Tooltip("Speed movement of enemy")]
    public float Speed;
    [Tooltip("Armor defense enemy")]
    public int Armor;
    [Tooltip("Coin get when enemy die")]
    public int CoinReceiver;
    [Tooltip("Dame Attack when enemy moving to homebase")]
    public int DamageToDefense;

}
