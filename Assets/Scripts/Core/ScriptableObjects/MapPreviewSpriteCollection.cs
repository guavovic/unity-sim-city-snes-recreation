using UnityEngine;

[CreateAssetMenu(fileName = "MapPreviewSpriteCollection", menuName = "Scriptable Objects/Map Preview Sprite Collection", order = 1)]
public sealed class MapPreviewSpriteCollection : ScriptableObject
{
    [Header("Map Preview Sprites")]
    public Sprite[] PreviewSprites;
}