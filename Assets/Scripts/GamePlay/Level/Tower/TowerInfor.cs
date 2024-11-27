using System;
using System.Collections;
using System.Collections.Generic;
using DefenderOfAsbara.UI;
using ObserverExtentision;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerInfor : ViewBase
{
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Button _sellButton;

    private TextMeshProUGUI _upgradeTextButton;
    private TextMeshProUGUI _sellTextButton;

    private TowerBase _currentTowerbaseSelect;

    private const string MAX_LV_TEXT = "MAX";

    [SerializeField] private int _currentCoin;

    private void Start()
    {
        Initialize();
    }

    public override void Initialize()
    {
        this._upgradeTextButton = _upgradeButton.GetComponentInChildren<TextMeshProUGUI>();
        this._sellTextButton = _sellButton.GetComponentInChildren<TextMeshProUGUI>();
        this.RegisterListener(EventID.OnChangeCoin, (param) => this.UpdateCoin((int)param));
        this.Hide();
    }

    public void Initialize(TowerBase towerBase)
    {
        if (towerBase == null) return;
        if (towerBase.IsMaxLevel)
        {
            DisplayText(_upgradeTextButton, MAX_LV_TEXT);
            this._upgradeButton.enabled = false;
        }
        else
        {
            RemoveAllButtonEvent();
            this._currentTowerbaseSelect = towerBase;
            DisplayText(_upgradeTextButton, towerBase.GetNextCostLevel.ToString());
            this._upgradeButton.onClick.AddListener(towerBase.UpgradeTower);
            this._upgradeButton.onClick.AddListener(this.Hide);
            this.UpdateCoin(this._currentCoin);
        }

        _sellTextButton.text = towerBase.GetCurrentSellCostLevel + "";
        this._sellButton.onClick.AddListener(towerBase.SellTower);
        this._sellButton.onClick.AddListener(this.Hide);
    }

    public void RemoveAllButtonEvent()
    {
        this._upgradeButton.onClick.RemoveAllListeners();
        this._sellButton.onClick.RemoveAllListeners();
    }

    private void DisplayText(TextMeshProUGUI textComponent, string text)
    {
        textComponent.text = text;
    }


    private void UpdateCoin(int currentCoin)
    {
        _currentCoin = currentCoin;

        if (_currentTowerbaseSelect == null) return;
        if (_currentCoin <= _currentTowerbaseSelect.GetNextCostLevel)
        {
            this._upgradeTextButton.color = Color.red;
            this._upgradeButton.enabled = false;

        }
        else
        {
            this._upgradeTextButton.color = Color.white;
            this._upgradeButton.enabled = true;
        }
    }
}
