using GV.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SceneTransitionManager : LoadAsyncController
{
    [SerializeField] private List<SceneField> allSceneList;
    [Space(5)][ReadOnly][SerializeField] private SceneField currentScene;
    [Space(5)][SerializeField] private float fadeDuration;

    public SceneField CurrentScene { get { return currentScene; } private set { currentScene = value; } }

    public static SceneTransitionManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public SceneField FindScenePerName(SceneName sceneName) { return allSceneList.Find(s => s.SceneName == sceneName); }

    public void ExecuteSceneTransition(SceneName nextSceneName, SceneTransitionActions sceneTransitionActions)
    {
        //if (GameStateController.IsBusy())
        //    return;

        StartCoroutine(PerfomLoadSceneAsync(FindScenePerName(nextSceneName), sceneTransitionActions));
    }

    private IEnumerator PerfomLoadSceneAsync(SceneField nextScene, SceneTransitionActions sceneTransitionActions = null)
    {
        yield return StartCoroutine(UIFadeTransition.FadeInCoroutine(FindObjectOfType<UIFadeTransitionPanelSettings>(true), fadeDuration));

        if (sceneTransitionActions != null || sceneTransitionActions.ActionsBeforeSceneChange != null)
            foreach (var action in sceneTransitionActions.ActionsBeforeSceneChange)
                action?.Invoke();

        // Debug.Log($"Vai carregar uma nova cena! \n Nome da cena: {nextScene.SceneName}.");

        yield return StartCoroutine(base.LoadSceneAsyncCoroutine(nextScene));

        // Debug.Log($"A nova cena foi carregada com sucesso.");

        CurrentScene = nextScene;

        if (sceneTransitionActions != null || sceneTransitionActions.ActionsAfterSceneChange != null)
            foreach (var action in sceneTransitionActions.ActionsAfterSceneChange)
                action?.Invoke();

        yield return StartCoroutine(UIFadeTransition.FadeOutCoroutine(FindObjectOfType<UIFadeTransitionPanelSettings>(true), fadeDuration));
    }
}