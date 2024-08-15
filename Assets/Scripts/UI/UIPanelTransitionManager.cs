using System;
using System.Collections;
using UnityEngine;

public sealed class UIPanelTransitionManager : MonoBehaviour
{
    private UIPanelsManager _uIPanelsManager;
    private UIFadeTransitionPanelSettings _uIFadeTransitionPanelSettings;

    public static UIPanelTransitionManager Instance;

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

    private void Start()
    {
        _uIPanelsManager = FindObjectOfType<UIPanelsManager>();
        _uIFadeTransitionPanelSettings = FindObjectOfType<UIFadeTransitionPanelSettings>(true);
    }

    public void ExecutePanelTransition(NavigationActionType navigationAction, UIPanelType panelType, float fadeDuration = 0.5f, float betweenFadeDuration = 0.5f, params Action[] actionsBetweenFade)
    {
        StartCoroutine(PerformPanelTransitionCoroutine(navigationAction, panelType, fadeDuration, betweenFadeDuration, actionsBetweenFade));
    }

    private IEnumerator PerformPanelTransitionCoroutine(NavigationActionType navigationAction, UIPanelType panelType, float fadeDuration, float betweenFadeDuration, params Action[] actionsBetweenFade)
    {
        yield return StartCoroutine(UIFadeTransitionHelper.FadeInCoroutine(_uIFadeTransitionPanelSettings, fadeDuration));

        if (navigationAction == NavigationActionType.Next)
        {
            _uIPanelsManager.GoToPanel(panelType);
        }
        else if (navigationAction == NavigationActionType.Back)
        {
            _uIPanelsManager.PreviousPanel();
        }

        foreach (var action in actionsBetweenFade)
            action?.Invoke();

        yield return new WaitForSeconds(betweenFadeDuration);
        yield return StartCoroutine(UIFadeTransitionHelper.FadeOutCoroutine(_uIFadeTransitionPanelSettings, fadeDuration));
    }
}