using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Star : ViewBase
{
    [SerializeField] private Image _starImage;

    private void Awake()
    {
        Initialize();
    }

    public override void Initialize()
    {
        Hide();
    }
    public override void Show()
    {
        _starImage.gameObject.SetActive(true);
    }

    public override void Hide()
    {
        _starImage.gameObject.SetActive(false);
    }
}
