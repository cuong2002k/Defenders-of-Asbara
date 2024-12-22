using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ProcessBar : MonoBehaviour
{
    [SerializeField] private Gradient _gradientColor;
    [SerializeField] private float _filledSpeed = 1f;
    private Image _imageFilled;

    private Coroutine _processChangeAnimatior;

    private void Awake()
    {
        _imageFilled = GetComponent<Image>();
    }

    private void Start()
    {
        if (_imageFilled == null && _imageFilled.type != Image.Type.Filled)
        {
#if UNITY_EDITOR
            EditorGUIUtility.PingObject(this.gameObject);
#endif
            this.gameObject.SetActive(false);
        }
    }

    public void SetProcessBar(float value)
    {
        SetProcessBar(value, this._filledSpeed);
    }

    private void SetProcessBar(float value, float speed)
    {
        if (value < 0 || value > 1)
        {
            value = Mathf.Clamp01(value);
        }

        if (value != this._imageFilled.fillAmount)
        {
            if (_processChangeAnimatior != null)
            {
                StopCoroutine(_processChangeAnimatior);
            }

            _processChangeAnimatior = StartCoroutine(this.ProcessChangeAnimator(value, speed));
        }
    }

    private IEnumerator ProcessChangeAnimator(float value, float speed)
    {
        float time = 0f;
        float initFillAmount = _imageFilled.fillAmount;

        while (time < 1)
        {
            _imageFilled.fillAmount = Mathf.Lerp(initFillAmount, value, speed);
            time += Time.deltaTime * speed;
            _imageFilled.color = this._gradientColor.Evaluate(1 - _imageFilled.fillAmount);
            yield return null;
        }

        _imageFilled.fillAmount = value;
        _imageFilled.color = this._gradientColor.Evaluate(1 - _imageFilled.fillAmount);


    }

}
