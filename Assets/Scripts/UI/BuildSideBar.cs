using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSideBar : MonoBehaviour
{
  [SerializeField] private TowerSpawnButton towerSpawnButton;

  private void Start() {
    if(LevelManager.Instance.HasInstance())
    {
      foreach(TowerBase tower in LevelManager.Instance.TowerLibrary.Towers)
      {
        TowerSpawnButton button = Instantiate(this.towerSpawnButton, this.transform);
        button.InitSpawnButton(tower);
      }
    }
  }
}
