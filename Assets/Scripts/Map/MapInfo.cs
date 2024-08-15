using UnityEngine;

public sealed class MapInfo
{
    public int Id { get; private set; }
    public Sprite PreviewSprite { get; private set; }
    public Texture2D MapTexture { get; private set; }
    public bool IsIsland { get; private set; }
    public MapGrid MapGrid { get; private set; }
    public Bounds Bounds { get; private set; }
    public TerrainData TerrainData { get; private set; } = new TerrainData();

    public MapInfo(int id, Sprite previewSprite, bool isIsland, MapGrid mapGrid)
    {
        Id = id;
        PreviewSprite = previewSprite;
        IsIsland = isIsland;
        MapGrid = mapGrid;
    }

    public void SetPreviewSprite(Sprite previewSprite) { PreviewSprite = previewSprite; }
    public void SetBounds(Bounds bounds) { Bounds = bounds; }
    public void SetMapTexture(Texture2D mapTexture) { MapTexture = mapTexture; }
}
