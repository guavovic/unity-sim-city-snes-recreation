using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public sealed class UIButtonNavigation : MonoBehaviour
{
    [SerializeField] private UIPanelType panelType = UIPanelType.None;
    [SerializeField] private NavigationActionType navigationAction = NavigationActionType.None;

    private UIPanelTransitionManager _uItransitionManager;

    private void Awake()
    {
        _uItransitionManager = FindObjectOfType<UIPanelTransitionManager>();
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        _uItransitionManager.ExecutePanelTransition(navigationAction, panelType);
    }
}
