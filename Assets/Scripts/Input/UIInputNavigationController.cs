using GV.Extensions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class UIInputNavigationController : MonoBehaviour
{
    private enum Direction { Up, Down, Left, Right }

    [ReadOnly][SerializeField] private Button _currentButtonSelected;
    private UICursorController _uICursorController;
    private UIPanelTransitionManager _uIPanelTransitionManager;

    public static UIInputNavigationController Instance { get; private set; }

    private void Awake()
    {
        _uICursorController ??= new UICursorController();
        _uIPanelTransitionManager ??= UIPanelTransitionManager.Instance;
        _uICursorController?.UICursorExist();

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

    private void Start()
    {
        _currentButtonSelected = GetRandomButton();
        _uICursorController.UpdateUICursorPosition(_currentButtonSelected.transform.position);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SelectNextButton(Direction.Down);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SelectNextButton(Direction.Up);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SelectNextButton(Direction.Left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SelectNextButton(Direction.Right);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            _uIPanelTransitionManager.ExecutePanelTransition(NavigationActionType.Back, UIPanelType.None);
            // Debug.Log("Ação cancelada");
        }
        else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            _currentButtonSelected.onClick.Invoke();
            // Debug.Log("Ação confirmada no " + currentButton.name);
        }
    }

    private void SelectNextButton(Direction direction)
    {
        Button nextButton = GetNearestButtonInDirection(direction);

        if (nextButton != null)
        {
            _currentButtonSelected = nextButton;
            _uICursorController.UpdateUICursorPosition(_currentButtonSelected.transform.position);
        }
        else
        {
            // Debug.Log("Nenhum botão encontrado na direção " + direction);
        }
    }

    private Button GetNearestButtonInDirection(Direction direction)
    {
        RectTransform currentRect = _currentButtonSelected.GetComponent<RectTransform>();
        Vector2 currentPosition = currentRect.anchoredPosition;

        List<Button> buttons = new List<Button>();
        buttons.AddRange(FindObjectsOfType<Button>());
        List<Button> validButtons = new List<Button>();

        foreach (Button button in buttons)
        {
            if (button.gameObject == _currentButtonSelected)
                continue;

            RectTransform rect = button.GetComponent<RectTransform>();
            Vector2 position = rect.anchoredPosition;

            bool isValid = false;

            switch (direction)
            {
                case Direction.Up:
                    isValid = position.y > currentPosition.y;
                    break;
                case Direction.Down:
                    isValid = position.y < currentPosition.y;
                    break;
                case Direction.Left:
                    isValid = position.x < currentPosition.x;
                    break;
                case Direction.Right:
                    isValid = position.x > currentPosition.x;
                    break;
            }

            if (isValid)
                validButtons.Add(button);
        }

        if (validButtons.Count == 0)
            return null;

        Button nearestButton = null;
        float nearestDistance = float.MaxValue;

        foreach (Button button in validButtons)
        {
            RectTransform rect = button.GetComponent<RectTransform>();
            Vector2 position = rect.anchoredPosition;
            float distance = Vector2.Distance(currentPosition, position);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestButton = button;
            }
        }

        return nearestButton;
    }

    private Button GetRandomButton()
    {
        List<Button> buttons = new List<Button>();
        buttons.AddRange(FindObjectsOfType<Button>());

        if (buttons.Count == 0)
            return null;

        return buttons[Random.Range(0, buttons.Count)];
    }
}

public static class SpriteHelper
{
    public static Vector2 CalculateTopLeftAdjustedPosition(Vector2 centerPosition, Image cursorImage)
    {
        Vector2 spriteSize = cursorImage.sprite.bounds.size;
        return new Vector2(centerPosition.x + (spriteSize.x / 2), centerPosition.y - (spriteSize.y / 2));
    }
}