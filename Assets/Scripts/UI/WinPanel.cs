using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPanel : ViewBase
{
    [SerializeField] private Star[] _stars;

    [SerializeField] private Button _back;
    [SerializeField] private Button _reset;
    [SerializeField] private Button _next;

    [SerializeField] private ScoreUI killScoreUI;

    private SceneName _sceneName;

    private void Start()
    {
        this.Initialize();
    }

    public override void Initialize()
    {
        // this.Hide();
        _back.onClick.AddListener(this.BackMainMenu);
        _reset.onClick.AddListener(this.Reset);
        _next.onClick.AddListener(NextLevel);
    }

    public void Initialize(int star, int killScore, SceneName sceneName)
    {
        killScore = killScore + 50;
        StartCoroutine(ShowStarReward(star));
        if (killScoreUI != null)
        {
            killScoreUI.SetScoreText(killScore);
        }

        _sceneName = sceneName;

    }

    private void BackMainMenu()
    {
        Loader.LoadScene(SceneName.Map);
    }

    private void Reset()
    {

        Loader.LoadScene(_sceneName);
    }

    private void NextLevel()
    {
        LevelMenuSelector._currentLevel++;
        Loader.LoadScene("Level" + LevelMenuSelector._currentLevel);

    }

    private IEnumerator ShowStarReward(int star)
    {
        for (int i = 0; i < _stars.Length; i++)
        {
            if (i < star)
            {
                _stars[i].Show();
                yield return new WaitForSeconds(0.5f);
            }
        }
    }


    private void OnDisable()
    {
        _back.onClick.RemoveListener(this.BackMainMenu);
        _reset.onClick.RemoveListener(this.Reset);
        _next.onClick.RemoveListener(NextLevel);

    }
}
