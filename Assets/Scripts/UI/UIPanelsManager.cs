using System.Collections.Generic;
using UnityEngine;

public sealed class UIPanelsManager : MonoBehaviour
{
    [SerializeField] private List<UIPanel> uIPanelModels;

    private Stack<UIPanel> _panelStack;
    private UIPanel _currentPanelActive;

    public UIPanel FindUIPanelPerType(UIPanelType panelType) => uIPanelModels.Find(panel => panel.Type == panelType);
    public UIPanelType GetCurrentActivePanelTType() => _currentPanelActive.Type;

    private void Awake()
    {
        _panelStack = new Stack<UIPanel>();
    }

    private void Start()
    {
        if (uIPanelModels.Count > 0)
        {
            _currentPanelActive = FindUIPanelPerType(UIPanelType.ChooseGameMode);
            _currentPanelActive.Panel.SetActive(true);
            _panelStack.Push(_currentPanelActive);
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        foreach (var panel in uIPanelModels)
        {
            panel.SetName(panel.Type.ToString());
        }
    }
#endif

    public void GoToPanel(UIPanelType panelType)
    {
        UIPanel nextPanel = FindUIPanelPerType(panelType);

        if (nextPanel != null && nextPanel != _currentPanelActive)
        {
            _currentPanelActive.Panel.SetActive(false);
            _currentPanelActive = nextPanel;
            _currentPanelActive.Panel.SetActive(true);
            _panelStack.Push(_currentPanelActive);
        }
    }

    public void PreviousPanel()
    {
        if (_panelStack.Count > 1)
        {
            _currentPanelActive.Panel.SetActive(false);
            _panelStack.Pop();
            _currentPanelActive = _panelStack.Peek();
            _currentPanelActive.Panel.SetActive(true);
        }
    }
}
