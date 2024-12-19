using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneNavigator : MonoBehaviour
{
    [SerializeField] private SceneName sceneName;

    private Button _button;

    private void Awake()
    {
        this._button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        this._button.onClick.AddListener(this.Onclick);
    }

    private void Onclick()
    {
        Loader.LoadScene(sceneName);
        Debug.Log("Open");
    }

    private void OnDisable()
    {
        this._button.onClick.RemoveListener(Onclick);
    }
}
