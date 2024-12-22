using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneNavigator : MonoBehaviour
{
    [SerializeField] private SceneName sceneName;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _clickSound;

    private Button _button;

    private void Awake()
    {
        this._button = GetComponent<Button>();
        this._audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        this._button.onClick.AddListener(this.Onclick);
    }

    private void Onclick()
    {
        StartCoroutine(this.NextScene());

    }

    IEnumerator NextScene()
    {
        _audioSource.PlayOneShot(_clickSound);
        while (_audioSource.isPlaying)
        {
            yield return null;
        }
        Loader.LoadScene(sceneName);
    }

    private void OnDisable()
    {
        this._button.onClick.RemoveListener(Onclick);
    }
}
