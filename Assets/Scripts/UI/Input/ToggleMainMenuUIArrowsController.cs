using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct MainMenuUIArrowsModel
{
    [SerializeField] private List<MainMenuUIArrow> mainMenuUIArrows;
    public readonly List<MainMenuUIArrow> MainMenuUIArrows { get { return mainMenuUIArrows; } }
}

public sealed class ToggleMainMenuUIArrowsController : MonoBehaviour
{
    [SerializeField] private MainMenuUIArrowsModel mainMenuUIArrowsModel;

    private int _currentGameModeTarget = 0;

    private void OnEnable()
    {
        // Disable all main menu UI arrows
        foreach (var mainMenuUIArrow in mainMenuUIArrowsModel.MainMenuUIArrows)
            mainMenuUIArrow.gameObject.SetActive(false);

        ToggleMainMenuUIArrows();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && _currentGameModeTarget > 0)
        {
            _currentGameModeTarget--;
            ToggleMainMenuUIArrows();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && _currentGameModeTarget < mainMenuUIArrowsModel.MainMenuUIArrows.Count - 1)
        {
            _currentGameModeTarget++;
            ToggleMainMenuUIArrows();
        }
    }

    private void ToggleMainMenuUIArrows()
    {
        for (var i = 0; i < mainMenuUIArrowsModel.MainMenuUIArrows.Count; i++)
            mainMenuUIArrowsModel.MainMenuUIArrows[i].gameObject.SetActive(i == _currentGameModeTarget);
    }
}