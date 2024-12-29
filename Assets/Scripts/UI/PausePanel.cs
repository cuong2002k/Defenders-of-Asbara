using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : ViewBase
{
    [SerializeField] private Button _resume;

    private void OnEnable()
    {
        _resume.onClick.AddListener(this.Resume);
    }

    private void OnDisable()
    {
        _resume.onClick.RemoveListener(this.Resume);
    }

    public override void Initialize()
    {

    }

    private void Resume()
    {
        GameManager.Instance.ChangeState(GameState.PLAY);
        this.Hide();
    }

    public override void Show()
    {
        base.Show();
        GameManager.Instance.ChangeState(GameState.PAUSE);

    }
}
