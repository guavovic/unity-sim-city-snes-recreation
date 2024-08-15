using System.Collections.Generic;
using UnityEngine;

public sealed class UIPanelsManager : MonoBehaviour
{
    [SerializeField] private List<UIPanel> uIPanelModels;

    private readonly Stack<UIPanel> _panelStack = new Stack<UIPanel>();
    private UIPanel _currentPanelActive;

    private void Start()
    {
        if (uIPanelModels.Count > 0)
        {
            _currentPanelActive = FindUIPanelPerType(UIPanelType.ChooseGameMode);

            if (_currentPanelActive != null && _currentPanelActive.Panel != null)
            {
                _currentPanelActive.Panel.SetActive(true);
                _panelStack.Push(_currentPanelActive);
            }
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (uIPanelModels != null)
        {
            foreach (var panel in uIPanelModels)
            {
                panel.SetName(panel.Type.ToString());
            }
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

    public UIPanel FindUIPanelPerType(UIPanelType panelType) => uIPanelModels.Find(panel => panel.Type == panelType);

    public UIPanelType GetCurrentActivePanelTType() => _currentPanelActive.Type;
}
