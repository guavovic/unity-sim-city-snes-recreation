using System;
using System.Collections;
using UnityEngine;

public sealed class UIPanelTransitionManager : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float betweenFadeDuration = 0.5f;

    private UIPanelsManager _uIPanelsManager;
    private UIFadeTransitionPanelSettings _uIFadeTransitionPanelSettings;

    public static UIPanelTransitionManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(Instance);
        }
    }

    private void Start()
    {
        _uIPanelsManager = FindObjectOfType<UIPanelsManager>();
        _uIFadeTransitionPanelSettings = FindObjectOfType<UIFadeTransitionPanelSettings>(true);
    }

    public void ExecutePanelTransition(NavigationActionType navigationAction, UIPanelType panelType, params Action[] actionsBetweenFade)
    {
        StartCoroutine(PerformPanelTransitionCoroutine(navigationAction, panelType, actionsBetweenFade));
    }

    private IEnumerator PerformPanelTransitionCoroutine(NavigationActionType navigationAction, UIPanelType panelType, params Action[] actionsBetweenFade)
    {
        yield return StartCoroutine(UIFadeTransition.FadeInCoroutine(_uIFadeTransitionPanelSettings, fadeDuration));

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
        yield return StartCoroutine(UIFadeTransition.FadeOutCoroutine(_uIFadeTransitionPanelSettings, fadeDuration));
    }
}