using UnityEngine;

[CreateAssetMenu(fileName = "TerrainSprites", menuName = "Scriptable Objects/Terrain Sprites", order = 1)]
public sealed class TerrainSprites : ScriptableObject
{
    [Header("Terrain Sprites")]
    public Sprite LandSprite;
    public Sprite GrassSprite;
    public Sprite ForestSprite;
    public Sprite ShoreSprite;
    public Sprite WaterSprite;
}