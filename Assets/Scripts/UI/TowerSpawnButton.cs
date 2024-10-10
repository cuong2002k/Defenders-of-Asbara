using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerSpawnButton : MonoBehaviour
{
    private TowerBase _tower;

    public TextMeshProUGUI Text;
    public Button Button;

    private Action<TowerBase> onTapped;

    private void Start() {
      this.Button.onClick.AddListener(OnClick);
    }

    public void InitSpawnButton(TowerBase tower)
    {
      if(tower == null) return;

      this._tower = tower;
      this.Text.text = this._tower.name;
      onTapped += PlacementSystem.Instance.SetTowerPlace;
    }

    private void OnClick()
    {
      onTapped?.Invoke(this._tower);
    }


    private void OnDisable() {
      if(onTapped != null)
      {
        onTapped -= PlacementSystem.Instance.SetTowerPlace;
      }
    }
}
