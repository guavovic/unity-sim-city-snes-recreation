using UnityEngine;
using TMPro;

public sealed class KeyboardInputTextController : MonoBehaviour
{
    private const char DEFAULT_CHAR = '_';
    private const int MAX_CHARACTERS = 8;

    [SerializeField] private TMP_Text inputTextInDisplay;

    public string InputText { get; private set; }
    public bool HasInsertedCharacter => InputText[0] != DEFAULT_CHAR;

    public void UpdateInputText(string letter)
    {
        int index = InputText.IndexOf(DEFAULT_CHAR);

        if (index != -1)
        {
            InputText = InputText.Substring(0, index) + letter + InputText.Substring(index + 1);
            UpdateInputTextDisplay();
        }
    }

    public void AddSpace()
    {
        int index = InputText.IndexOf(DEFAULT_CHAR);

        if (index != -1)
        {
            InputText = InputText.Substring(0, index) + " " + InputText.Substring(index + 1);
            UpdateInputTextDisplay();
        }
    }

    public void RemoveLastCharacter()
    {
        for (int i = InputText.Length - 1; i >= 0; i--)
        {
            if (InputText[i] != DEFAULT_CHAR)
            {
                InputText = InputText.Substring(0, i) + DEFAULT_CHAR + InputText.Substring(i + 1);
                UpdateInputTextDisplay();
                return;
            }
        }
    }

    public void ClearInputText()
    {
        InputText = "";

        for (int i = 0; i < MAX_CHARACTERS; i++)
        {
            InputText += DEFAULT_CHAR;
        }

        UpdateInputTextDisplay();
    }

    private void UpdateInputTextDisplay()
    {
        if (inputTextInDisplay != null)
            inputTextInDisplay.text = InputText;
    }
}
