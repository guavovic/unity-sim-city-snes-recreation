using GV.Extensions;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class LoadAsyncController : MonoBehaviour
{
    protected AsyncOperation AsyncLoad { get; private set; }

    protected IEnumerator LoadSceneAsyncCoroutine(SceneField sceneField)
    {
        AsyncLoad = SceneManager.LoadSceneAsync(sceneField);
        AsyncLoad.allowSceneActivation = true; // deixar isso falso

        while (!AsyncLoad.isDone)
        {
            Debug.Log("Carregando nova cena...");
            yield return null;
        }
    }
}