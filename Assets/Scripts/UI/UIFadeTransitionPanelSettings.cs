using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(CanvasGroup))]
public sealed class UIFadeTransitionPanelSettings : MonoBehaviour
{
    [SerializeField] private Image panelImage;
    [SerializeField] private CanvasGroup panelCanvasGroup;

    public Image PanelImage { get { return panelImage; } }
    public CanvasGroup PanelCanvasGroup { get { return panelCanvasGroup; } }

    private void Awake()
    {
        this.gameObject.SetActive(false);
    }
}
