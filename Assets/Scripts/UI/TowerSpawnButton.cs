using System;
using System.Collections;
using System.Collections.Generic;
using ObserverExtentision;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerSpawnButton : MonoBehaviour
{
    private TowerBase _tower;
    public TextMeshProUGUI Text;
    public Button Button;

    private void Start() {
      this.Button.onClick.AddListener(OnClick);
    }

    public void InitSpawnButton(TowerBase tower)
    {
      if(tower == null) return;

      this._tower = tower;
      this.Text.text = this._tower.name;
      
    }

    private void OnClick()
    {
      this.PostEvent(EventID.StartBuilding, this._tower);
    }
}
