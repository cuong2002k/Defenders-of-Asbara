using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    TextMeshProUGUI _coinTextUI;
    // Start is called before the first frame update
    void Start()
    {
        _coinTextUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        this._coinTextUI.text = LevelManager.Instance.CoinLevel +"";
    }
}