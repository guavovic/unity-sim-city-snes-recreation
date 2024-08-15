using System.Collections.Generic;
using UnityEngine;

public sealed class MapInitializer : MonoBehaviour
{
    private GameSettings _gameSettings;

    public static MapInitializer Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _gameSettings = GameManager.Instance.GameSettings;

        if (!_gameSettings.CityCreated())
        {
            Debug.Log("City not set. Creating a temporary city.");

            CreateTemporaryCity();
            InitializeMap();

            CameraController.Instance.Initialize();
        }
    }

    public void InitializeMap()
    {
        ProcessMapTexture();
        InstantiateMapTiles();
        ConfigureMapTiles();
        DefineMapBounds();
    }

    private void ProcessMapTexture()
    {
        var mapTexture = TerrainAnalyzerHelper.ConvertSpriteToTexture(_gameSettings.City.MapInfo.PreviewSprite);
        _gameSettings.City.MapInfo.SetMapTexture(mapTexture);
        TerrainAnalyzerHelper.AnalyzeTexture(_gameSettings.City.MapInfo);
    }

    private void InstantiateMapTiles()
    {
        var mapMatrix = _gameSettings.City.MapInfo.MapGrid;

        for (int x = 0; x < mapMatrix.Width; x++)
        {
            for (int y = 0; y < mapMatrix.Height; y++)
            {
                MapTile tile = mapMatrix.Tiles[x, y];

                tile.AssignGameObject(new GameObject());
                tile.SetParent(transform);
                tile.AssignSpriteRenderer(tile.GameObject.AddComponent<SpriteRenderer>());
            }
        }
    }

    private void ConfigureMapTiles()
    {
        const float tileScale = 0.356f;

        var mapMatrix = _gameSettings.City.MapInfo.MapGrid;

        for (int x = 0; x < mapMatrix.Width; x++)
        {
            for (int y = 0; y < mapMatrix.Height; y++)
            {
                var tile = mapMatrix.Tiles[x, y];

                tile.UpdateMatrixIndex(x, y);
                tile.SetPosition(new Vector2(x * tileScale, y * tileScale));
                tile.SetScale(new Vector2(13 * tileScale, 13 * tileScale));

                AssignTileSpriteAndName(tile);
            }
        }
    }

    private void AssignTileSpriteAndName(MapTile tile)
    {
        string name;
        TerrainSprites terrainSprites = ScriptableObjectLoader.Load<TerrainSprites>();
        Sprite tileSprite;

        switch ((TerrainType)tile.Value)
        {
            case TerrainType.Forest:
                name = TerrainType.Forest.ToString();
                tileSprite = terrainSprites.ForestSprite;
                break;
            case TerrainType.Water:
                name = TerrainType.Water.ToString();
                tileSprite = terrainSprites.WaterSprite;
                break;
            default: // TerrainType.Land
                name = TerrainType.Land.ToString();
                tileSprite = terrainSprites.LandSprite;
                break;
        }

        tile.UpdateName(name);
        tile.AssignSprite(tileSprite);
    }

    private void DefineMapBounds()
    {
        var bounds = CalculateMapBounds();
        _gameSettings.City.MapInfo.SetBounds(bounds);
    }

    private Bounds CalculateMapBounds()
    {
        var mapMatrix = _gameSettings.City.MapInfo.MapGrid;
        List<Renderer> renderers = new List<Renderer>(mapMatrix.Tiles.Length);

        foreach (var tile in mapMatrix.Tiles)
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

    private void CreateTemporaryCity()
    {
        City newCity = new City();
        int index = 0;

        MapInfo mapInfo = new MapInfo(
            id: index,
            previewSprite: ScriptableObjectLoader.Load<MapPreviewSpriteCollection>().PreviewSprites[index],
            isIsland: false,
            mapGrid: new MapGrid(MapConstants.MAP_WIDTH, MapConstants.MAP_HEIGHT));

        newCity.SetMapInfo(mapInfo);
        _gameSettings.SetCity(newCity);
    }
}
