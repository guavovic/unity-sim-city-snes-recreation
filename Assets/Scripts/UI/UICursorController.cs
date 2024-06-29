using UnityEngine;

public sealed class UICursorController
{
    public UICursor UICursor { get; private set; }

    public void UICursorExist()
    {
        if (UICursor == null)
            UICursor = FindUICursor();
    }

    public void UpdateUICursorPosition(Vector2 newPosition)
    {
        // Vector2 adjustedPosition = SpriteHelper.CalculateTopLeftAdjustedPosition(newPosition, UICursor.Image);
        UICursor.SetPosition(newPosition);
    }

    public UICursor FindUICursor()
    {
        var uICursor = Object.FindObjectOfType<UICursor>();

        if (uICursor != null)
        {
            Debug.Log("UICursor encontrado!");
            return uICursor;
        }
        else
        {
            Debug.Log("UICursor não encontrado!");
            return null;
        }
    }
}