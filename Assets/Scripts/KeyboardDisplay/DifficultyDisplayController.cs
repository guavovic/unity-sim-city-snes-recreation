using UnityEngine;

public sealed class DifficultyDisplayController : MonoBehaviour
{
    public DifficultyType DifficultySelected { get; private set; } = DifficultyType.None;
    public bool DifficultyConfirmed { get; private set; }

}