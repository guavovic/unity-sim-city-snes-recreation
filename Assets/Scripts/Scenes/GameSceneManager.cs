using GV.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameSceneManager : LoadAsyncController
{
    [SerializeField] private List<SceneField> allSceneList;
    [Space(5)][ReadOnly][SerializeField] private SceneField currentScene;
    [Space(5)][SerializeField] private float fadeDuration;

    public SceneField CurrentScene { get { return currentScene; } private set { currentScene = value; } }

    public static GameSceneManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public void InitializeScene(SceneName sceneName, SceneTransitionActions sceneTransitionActions)
    {
        GameStateController.SetState(GameState.Busy);

        ExecuteSceneTransition(sceneName, sceneTransitionActions);

        GameStateController.SetState(GameState.None);
    }

    public SceneField FindScenePerName(SceneName sceneName) { return allSceneList.Find(s => s.SceneName == sceneName); }

    public void ExecuteSceneTransition(SceneName sceneName, SceneTransitionActions sceneTransitionActions)
    {
        StartCoroutine(PerfomLoadSceneAsync(FindScenePerName(sceneName), sceneTransitionActions));
    }

    private IEnumerator PerfomLoadSceneAsync(SceneField nextScene, SceneTransitionActions sceneTransitionActions)
    {
        yield return StartCoroutine(UIFadeTransitionHelper.FadeInCoroutine(FindObjectOfType<UIFadeTransitionPanelSettings>(true), fadeDuration));

        if (sceneTransitionActions.ActionsBeforeSceneChange != null)
        {
            foreach (var action in sceneTransitionActions.ActionsBeforeSceneChange)
                action?.Invoke();
        }

        // Debug.Log($"Vai carregar uma nova cena! \n Nome da cena: {nextScene.SceneName}.");

        yield return StartCoroutine(base.LoadSceneAsyncCoroutine(nextScene));

        // Debug.Log($"A nova cena foi carregada com sucesso.");

        CurrentScene = nextScene;

        if (sceneTransitionActions.ActionsAfterSceneChange != null)
        {
            foreach (var action in sceneTransitionActions.ActionsAfterSceneChange)
                action?.Invoke();
        }

        yield return StartCoroutine(UIFadeTransitionHelper.FadeOutCoroutine(FindObjectOfType<UIFadeTransitionPanelSettings>(true), fadeDuration));
    }
}