using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreTextUI;

    public void SetScoreText(int score)
    {
        StartCoroutine(ScoreAnimation(score));
    }

    private IEnumerator ScoreAnimation(int score)
    {
        int value = 0;

        while (value < score)
        {
            value++;
            _scoreTextUI.text = value + "";
            yield return new WaitForSeconds(0.01f);
        }
    }
}
