using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public class LoadingMonoBehaviour : MonoBehaviour { }
    private static AsyncOperation asyncOperation;

    private static Action onLoaderCallBack;


    public static void LoadScene(SceneName sceneName)
    {

        onLoaderCallBack = () =>
        {
            GameObject loadingGameObject = new GameObject("Loading game object");
            loadingGameObject.AddComponent<LoadingMonoBehaviour>().StartCoroutine(LoadSceneAsync(sceneName));
        };

        SceneManager.LoadScene(SceneName.Loading.ToString());
    }

    private static IEnumerator LoadSceneAsync(SceneName sceneName)
    {
        yield return null;

        asyncOperation = SceneManager.LoadSceneAsync(sceneName.ToString());

        while (!asyncOperation.isDone)
        {

            yield return null;
        }
    }

    public static void LoaderCallback()
    {
        if (onLoaderCallBack != null)
        {
            onLoaderCallBack.Invoke();
            onLoaderCallBack = null;
        }
    }

    public static float GetLoadingProcess
    {
        get
        {
            if (asyncOperation != null)
            {
                return asyncOperation.progress;
            }
            return 1f;
        }
    }

}

public enum SceneName
{
    MenuMain,
    Map,
    Level1,
    Loading
}
