using System.Collections;
using UnityEngine;

public sealed class UIPanelTransitionManager : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float betweenFadeDuration = 0.5f;

    [SerializeField] private GameObject fadeTransitionPanel;

    private CanvasGroup _fadeTransitionPanelCanvasGroup;

    private UIPanelsManager _uIPanelsManager;
    private KeyboardInputDisplayManager _keyboardInputDisplayManager;

    private void Awake()
    {
        _uIPanelsManager = FindObjectOfType<UIPanelsManager>();
        _keyboardInputDisplayManager = FindObjectOfType<KeyboardInputDisplayManager>();

        _fadeTransitionPanelCanvasGroup = fadeTransitionPanel.AddComponent<CanvasGroup>();
        _fadeTransitionPanelCanvasGroup.alpha = 0;
        _fadeTransitionPanelCanvasGroup.interactable = true;
    }

    private void Start()
    {
        fadeTransitionPanel.SetActive(false);
    }

    public void ShowNextPanel(UIPanelType panelType)
    {
        StartCoroutine(TransitionToPanel(panelType));
    }

    public void ShowPreviousPanel()
    {
        StartCoroutine(TransitionToPreviousPanel());
    }

    private IEnumerator TransitionToPanel(UIPanelType panelType)
    {
        yield return StartCoroutine(FadeIn());

        _uIPanelsManager.GoToPanel(panelType);
        _keyboardInputDisplayManager.UpdateDisplayState();

        yield return new WaitForSeconds(betweenFadeDuration);
        yield return StartCoroutine(FadeOut());
    }

    private IEnumerator TransitionToPreviousPanel()
    {
        yield return StartCoroutine(FadeIn());

        _uIPanelsManager.PreviousPanel();
        _keyboardInputDisplayManager.UpdateDisplayState();

        yield return new WaitForSeconds(betweenFadeDuration);
        yield return StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float startAlpha = _fadeTransitionPanelCanvasGroup.alpha;
        float rate = 1.0f / fadeDuration;
        float progress = 0.0f;

        while (progress < 1.0f)
        {
            _fadeTransitionPanelCanvasGroup.alpha = Mathf.Lerp(startAlpha, 0, progress);
            progress += rate * Time.deltaTime;
            yield return null;
        }

        _fadeTransitionPanelCanvasGroup.alpha = 0;
        fadeTransitionPanel.SetActive(false);
    }

    private IEnumerator FadeIn()
    {
        float startAlpha = _fadeTransitionPanelCanvasGroup.alpha;
        float rate = 1.0f / fadeDuration;
        float progress = 0.0f;

        fadeTransitionPanel.SetActive(true);

        while (progress < 1.0f)
        {
            _fadeTransitionPanelCanvasGroup.alpha = Mathf.Lerp(startAlpha, 1, progress);
            progress += rate * Time.deltaTime;
            yield return null;
        }

        _fadeTransitionPanelCanvasGroup.alpha = 1;
    }
}