using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeathUI : MonoBehaviour
{
    private TextMeshProUGUI _heathText;
    private int _maxHeath = 0;


    private void Start()
    {
        this._heathText = GetComponentInChildren<TextMeshProUGUI>();
        this._maxHeath = LevelManager.Instance.Health;
    }

    // Update is called once per frame
    void Update()
    {
        this._heathText.text = LevelManager.Instance.Health + "/" + _maxHeath;
    }
}
