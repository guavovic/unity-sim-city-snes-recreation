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
    private DifficultyDisplayController _difficultyDisplayController;
    private UIPanelTransitionManager _panelTransitionManager;

    private DisplayState _currentDisplayState = DisplayState.CityName;

    public string CityName { get; private set; }
    public string MayorName { get; private set; }
    public DifficultyType DifficultySelected { get; private set; } = DifficultyType.None;

    private void Awake()
    {
        _keyboardInputTextController = FindObjectOfType<KeyboardInputTextController>();
        _difficultyDisplayController = FindObjectOfType<DifficultyDisplayController>();
        _panelTransitionManager = FindObjectOfType<UIPanelTransitionManager>();
    }

    private void OnEnable()
    {
        UpdateDisplayState();
    }

    public void SaveCurrentInfos()
    {
        switch (_currentDisplayState)
        {
            case DisplayState.CityName:

                if (!_keyboardInputTextController.HasInsertedCharacter)
                    return;

                CityName = _keyboardInputTextController.InputText;
                SetNextDisplayState(DisplayState.MayorName);
                break;

            case DisplayState.MayorName:

                if (!_keyboardInputTextController.HasInsertedCharacter)
                    return;

                MayorName = _keyboardInputTextController.InputText;
                SetNextDisplayState(DisplayState.DifficultyType);
                break;

            case DisplayState.DifficultyType:
                DifficultySelected = _difficultyDisplayController.DifficultySelected;
                SetNextDisplayState(DisplayState.ConfirmDifficulty);
                break;

            case DisplayState.ConfirmDifficulty:

                if (!_difficultyDisplayController.DifficultyConfirmed)
                {
                    GameManager.Instance.InitializeGameplayScene();
                }
                else
                {
                    DifficultySelected = DifficultyType.None;
                    SetNextDisplayState(DisplayState.DifficultyType);
                }

                return;
        }

        // _panelTransitionManager.ExecutePanelTransition(NavigationActionType.Next, UIPanelType.KeyboardDisplay, UpdateDisplayState);
    }

    public void UpdateDisplayState()
    {
        switch (_currentDisplayState)
        {
            case DisplayState.CityName:
                inputTextInDisplay.text = "Enter name of the City";
                break;

            case DisplayState.MayorName:
                inputTextInDisplay.text = "Enter name of the Mayor";
                _keyboardInputTextController.ClearInputText();
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

    private void SetNextDisplayState(DisplayState nextDisplayState)
    {
        _currentDisplayState = nextDisplayState;
        UpdateDisplayState();
    }
}