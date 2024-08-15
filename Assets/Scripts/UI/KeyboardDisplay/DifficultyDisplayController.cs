using UnityEngine;

public sealed class DifficultyDisplayController : MonoBehaviour
{
    public static GameDifficultyType DifficultySelected { get; private set; } = GameDifficultyType.None;
    public static bool DifficultyConfirmed { get; private set; }

}