using System;
using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    public GameSettings GameSettings { get; private set; } = new GameSettings();
    public GameStateController GameStateController { get; private set; } = new GameStateController();

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void InitializeGameplayScene()
    {
        GameManager.Instance.GameStateController.SetState(GameState.Busy);

        SceneTransitionManager.Instance.ExecuteSceneTransition(
           nextSceneName: SceneName.Gameplay,
           sceneTransitionActions: new SceneTransitionActions
           {
               ActionsBeforeSceneChange = new Action[]
               {
                   () => CityConfigLoader.Instance.InitializeCityData(),
               },

               ActionsAfterSceneChange = new Action[]
               {
                   () => MapLoader.Instance.InitializeLoad(),
                   () => MapCameraFocusManager.Instance.CenterCameraOnRandomPosition(),
                   () => CameraController.Instance.Initialize(),
               }
           });
        
        GameManager.Instance.GameStateController.SetState(GameState.None);
    }

    public void ReturnToMainMenu()
    {

    }
}
