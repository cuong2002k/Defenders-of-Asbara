using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TowerLibrary", menuName = "data/TowerLibrary")]
public class TowerLibrary : ScriptableObject
{
    [SerializeField]private TowerBase[] _towers;
    public TowerBase[] Towers => this._towers;
}
