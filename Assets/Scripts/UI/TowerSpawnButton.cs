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
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Cost;
    public Image Icon;
    public Button Button;

    private void Start()
    {
        this.Button.onClick.AddListener(OnClick);
        this.RegisterListener(EventID.OnChangeCoin, (param) => this.CanBuilding((int)param));
        this.CanBuilding(LevelManager.Instance.CoinLevel);
    }

    /// <summary>
    /// init towerbase to building button
    /// </summary>
    /// <param name="tower">Tower for button</param>
    public void InitSpawnButton(TowerBase tower)
    {
        if (tower == null) return;

        this._tower = tower;
        this.Name.text = this._tower.TowerConfig.Name;
        this.Icon.sprite = this._tower.TowerConfig.Icon;
        this.Cost.text = this._tower.CurrentTowerLevel.GetTowerCost + "";
    }

    /// <summary>
    /// Event when click button UI
    /// </summary>
    private void OnClick()
    {
        this.PostEvent(EventID.StartBuilding, this._tower);
    }

    /// <summary>
    /// Check coin can building
    /// </summary>
    /// <param name="CurrentCoin"></param>
    private void CanBuilding(int CurrentCoin)
    {

        if (this._tower.GetCurrentCostLevel <= CurrentCoin)
        {
            this.Button.enabled = true;
            this.Cost.color = Color.yellow;
        }
        else
        {
            this.Button.enabled = false;
            this.Cost.color = Color.red;

        }
    }

    /// <summary>
    /// remove event when disable
    /// </summary>
    private void OnDisable()
    {
        this.RemoveListener(EventID.OnChangeCoin, (param) => this.CanBuilding((int)param));
    }
}
