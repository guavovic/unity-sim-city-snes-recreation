using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public sealed class UIButtonNavigation : MonoBehaviour
{
    private enum NavigationActionType
    {
        None,
        Next,
        Back
    }

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
        if (navigationAction == NavigationActionType.Next)
        {
            _uItransitionManager.ShowNextPanel(panelType);
        }
        else if (navigationAction == NavigationActionType.Back)
        {
            _uItransitionManager.ShowPreviousPanel();
        }
    }
}
