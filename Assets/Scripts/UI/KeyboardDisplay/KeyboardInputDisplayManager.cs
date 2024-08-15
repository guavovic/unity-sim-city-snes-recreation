using System;
using TMPro;
using UnityEngine;

public sealed class KeyboardInputDisplayManager : MonoBehaviour
{
    private enum DisplayState
    {
        CityName,
        MayorName,
        DifficultyType,
        ConfirmDifficulty,
    }

    [SerializeField] private TMP_Text inputTextInDisplay;

    private KeyboardInputTextController _keyboardInputTextController;

    private DisplayState _currentDisplayState = DisplayState.CityName;

    private GameSettings _gameSettings;

    private void Awake()
    {
        _keyboardInputTextController = FindObjectOfType<KeyboardInputTextController>();
    }

    private void OnEnable()
    {
        UpdateDisplayState();
    }

    private void Start()
    {
        _gameSettings = GameManager.Instance.GameSettings;

        if (!_gameSettings.CityCreated())
            _gameSettings.SetCity(new City());
    }

    public void SaveCurrentInfos()
    {
        switch (_currentDisplayState)
        {
            case DisplayState.CityName:

                if (!KeyboardInputTextController.HasInsertedCharacter)
                    return;

                _gameSettings.City.SetCityName(KeyboardInputTextController.InputText);
                _currentDisplayState = DisplayState.MayorName;
                break;

            case DisplayState.MayorName:

                if (!KeyboardInputTextController.HasInsertedCharacter)
                    return;

                _gameSettings.City.SetMayorName(KeyboardInputTextController.InputText);
                _currentDisplayState = DisplayState.DifficultyType;
                break;

            case DisplayState.DifficultyType:
                _gameSettings.SetGameDifficultyType(DifficultyDisplayController.DifficultySelected);
                _currentDisplayState = DisplayState.ConfirmDifficulty;
                break;

            case DisplayState.ConfirmDifficulty:

                if (!DifficultyDisplayController.DifficultyConfirmed)
                {
                    GameSceneManager.Instance.InitializeScene(
                        sceneName: SceneName.Gameplay,
                        sceneTransitionActions: new SceneTransitionActions
                        {
                            ActionsBeforeSceneChange = new Action[]
                            {
                                () => {
                                    //Debug.Log("Setando variaveis do mapa selecionado...");

                                    var selectedMap = new MapInfo(
                                        id: CounterButtonsManager.MapIndex,
                                        previewSprite: MapPreviewDisplayManager.CurrentMapPreview,
                                        isIsland: false,
                                        mapGrid: new MapGrid(MapConstants.MAP_WIDTH, MapConstants.MAP_HEIGHT));

                                    //Debug.Log(
                                    //    "As variaveis do mapa selecionado foram setadas! " +
                                    //    $"\n " +
                                    //    $"\n Map index: {selectedMap.Id}. " +
                                    //    $"\n Is island? {selectedMap.IsIsland}. " +
                                    //    $"\n Map lenght: ({selectedMap.MapMatrix.MapTiles.Length}) - [ Width={selectedMap.MapMatrix.Width};Height={selectedMap.MapMatrix.Height}; ].");

                                    _gameSettings.City.SetMapInfo(selectedMap);
                                },

                                () => { GameStateController.SetState(GameState.Transitioning); }
                            },
                            ActionsAfterSceneChange = new Action[]
                            {
                                () => MapInitializer.Instance.InitializeMap(),
                                () => CameraController.Instance.Initialize(),
                                () => { GameStateController.SetState(GameState.None); }
                            }
                        });
                }
                else
                {
                    _gameSettings.SetGameDifficultyType(GameDifficultyType.None);
                    _currentDisplayState = DisplayState.DifficultyType;
                }

                break;
        }

        UpdateDisplayState();
    }

    private void UpdateDisplayState()
    {
        switch (_currentDisplayState)
        {
            case DisplayState.CityName:
                inputTextInDisplay.text = "Enter name of the City";
                break;

            case DisplayState.MayorName:
                inputTextInDisplay.text = "Enter name of the Mayor";
                break;

            case DisplayState.DifficultyType:
                inputTextInDisplay.text = "DifficultyType";
                break;

            case DisplayState.ConfirmDifficulty:
                inputTextInDisplay.text = "ConfirmDifficulty";
                break;
        }

        _keyboardInputTextController.ClearInputText();
    }
}