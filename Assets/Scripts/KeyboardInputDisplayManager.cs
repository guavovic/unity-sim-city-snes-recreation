using TMPro;
using UnityEngine;

public sealed class KeyboardInputDisplayManager : MonoBehaviour
{
    [SerializeField] private TMP_Text inputTextInDisplay;

    private KeyboardInputTextController _keyboardInputTextController;
    private UIPanelTransitionManager _panelTransitionManager;

    private DisplayState _currentDisplayState = DisplayState.CityName;

    public string CityName { get; private set; }
    public string MayorName { get; private set; }

    private enum DisplayState
    {
        CityName,
        MayorName
    }

    private void Awake()
    {
        _keyboardInputTextController = FindObjectOfType<KeyboardInputTextController>();
        _panelTransitionManager = FindObjectOfType<UIPanelTransitionManager>();
    }

    private void OnEnable()
    {
        UpdateDisplayState();
    }

    public void SaveCurrentInfos()
    {
        if (!_keyboardInputTextController.HasInsertedCharacter)
            return;

        switch (_currentDisplayState)
        {
            case DisplayState.CityName:
                CityName = _keyboardInputTextController.InputText;
                _currentDisplayState = DisplayState.MayorName;
                _panelTransitionManager.ShowNextPanel(UIPanelType.KeyboardDisplay);
                break;

            case DisplayState.MayorName:
                MayorName = _keyboardInputTextController.InputText;
                _currentDisplayState = DisplayState.CityName;
                _panelTransitionManager.ShowNextPanel(UIPanelType.DifficultySelection);
                break;
        }
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
                break;
        }

        _keyboardInputTextController.ClearInputText();
    }
}