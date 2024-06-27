using System.Collections.Generic;
using UnityEngine;

public sealed class MapLoader : MonoBehaviour
{
    // Sprites para diferentes tipos de terreno
    public Sprite landSprite;
    public Sprite grassSprite;
    public Sprite forestSprite;
    public Sprite shoreSprite;
    public Sprite waterSprite;

    private GameManager _gameManager;

    public static MapLoader Instance { get; private set; }

    private void Awake() { Instance = this; }

    private void Start() { _gameManager = GameManager.Instance; }

    public void InitializeLoad()
    {
        CreateMapTiles();
        InitializeMapTiles();
    }

    private void CreateMapTiles()
    {
        var mapMatrix = _gameManager.GameSettings.CurrentCity.MapData.MapMatrix;

        for (int y = 0; y < mapMatrix.GetLength(1); y++)
        {
            for (int x = 0; x < mapMatrix.GetLength(0); x++)
            {
                var tile = mapMatrix[x, y];
                tile.SetGameObject(new GameObject());
                tile.SetParent(transform);
                tile.SetSpriteRenderer(tile.GameObject.AddComponent<SpriteRenderer>());
            }
        }
    }

    private void InitializeMapTiles()
    {
        const float tileScale = 0.356f;

        var mapMatrix = _gameManager.GameSettings.CurrentCity.MapData.MapMatrix;
        int mapWidth = mapMatrix.GetLength(0);
        int mapHeight = mapMatrix.GetLength(1);

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                var tile = mapMatrix[x, y];
                string name;
                Sprite tileSprite;

                switch ((TerrainType)tile.Value)
                {
                    case TerrainType.Forest:
                        name = TerrainType.Forest.ToString();
                        tileSprite = forestSprite;
                        break;
                    case TerrainType.Water:
                        name = TerrainType.Water.ToString();
                        tileSprite = shoreSprite;
                        break;
                    default: // TerrainType.Land
                        name = TerrainType.Land.ToString();
                        tileSprite = landSprite;
                        break;
                }

                tile.SetName(name);
                tile.SetMatrixIndex(x, y);
                tile.SetPosition(new Vector2(x * tileScale, y * tileScale));
                tile.SetSprite(tileSprite);
                tile.SetScale(new Vector2(13 * tileScale, 13 * tileScale));
            }
        }

        _gameManager.GameSettings.CurrentCity.MapData.SetMapBounds(CalculateMapBounds());
    }

    private Bounds CalculateMapBounds()
    {
        var mapMatrix = _gameManager.GameSettings.CurrentCity.MapData.MapMatrix;
        List<Renderer> renderers = new List<Renderer>(mapMatrix.Length);

        foreach (var tile in mapMatrix)
        {
            if (tile.GameObject.TryGetComponent<Renderer>(out var renderer))
                renderers.Add(renderer);
        }

        if (renderers.Count == 0)
            return new Bounds(transform.position, Vector3.zero);

        Bounds bounds = renderers[0].bounds;

        for (int i = 1; i < renderers.Count; i++)
            bounds.Encapsulate(renderers[i].bounds);

        return bounds;
    }
}
