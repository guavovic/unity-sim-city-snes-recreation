using System.Collections;
using UnityEngine;

public static class UIFadeTransitionHelper
{
    public static IEnumerator FadeInCoroutine(UIFadeTransitionPanelSettings uIFadeTransitionPanel, float fadeDuration)
    {
        float startAlpha = uIFadeTransitionPanel.PanelCanvasGroup.alpha;
        float rate = 1.0f / fadeDuration;
        float progress = 0.0f;

        uIFadeTransitionPanel.gameObject.SetActive(true);

        while (progress < 1.0f)
        {
            uIFadeTransitionPanel.PanelCanvasGroup.alpha = Mathf.Lerp(startAlpha, 1, progress);
            progress += rate * Time.deltaTime;
            yield return null;
        }

        uIFadeTransitionPanel.PanelCanvasGroup.alpha = 1;
    }

    public static IEnumerator FadeOutCoroutine(UIFadeTransitionPanelSettings uIFadeTransitionPanel, float fadeDuration)
    {
        float startAlpha = uIFadeTransitionPanel.PanelCanvasGroup.alpha;
        float rate = 1.0f / fadeDuration;
        float progress = 0.0f;

        while (progress < 1.0f)
        {
            uIFadeTransitionPanel.PanelCanvasGroup.alpha = Mathf.Lerp(startAlpha, 0, progress);
            progress += rate * Time.deltaTime;
            yield return null;
        }

        uIFadeTransitionPanel.PanelCanvasGroup.alpha = 0;
        uIFadeTransitionPanel.gameObject.SetActive(false);
    }
}