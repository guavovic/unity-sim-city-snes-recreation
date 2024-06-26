using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    public GameSettings GameSettings { get; private set; }
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            GameSettings = new GameSettings();
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
