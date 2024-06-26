using System;
using UnityEngine;

[Serializable]
public class UIPanel
{
    [SerializeField] private string name;
    [SerializeField] private GameObject panel;
    [SerializeField] private UIPanelType type;

    public string Name { get { return name; } private set { name = value; } }
    public GameObject Panel { get { return panel; } private set { panel = value; } }
    public UIPanelType Type { get { return type; } private set { type = value; } }

    public void SetName(string name)
    {
        Name = name;
    }

    public void SetPanel(GameObject panel)
    {
        Panel = panel;
    }
}