using System;
using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    public GameSettings GameSettings { get; private set; } = new GameSettings();

    public static GameManager Instance { get; private set; }

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
}
