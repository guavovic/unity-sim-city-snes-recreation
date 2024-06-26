using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    // Criar um objeto para guardar e
    public Sprite landSprite;
    public Sprite grassSprite;
    public Sprite forestSprite;
    public Sprite shoreSprite;
    public Sprite waterSprite;

    private MapTile[,] _mapTiles;

    private GameManager _gameManager;


    private void Start()
    {
        _gameManager = GameManager.Instance;
        _mapTiles = _gameManager.GameSettings.CurrentCity.MapData.MapMatrix;

        GenerateMapMatrixGameObjects();
        SetupMapMatrixGameObjects();
    }

    private void GenerateMapMatrixGameObjects()
    {
        for (int y = 0; y < MapData.MAP_HEIGHT; y++)
        {
            for (int x = 0; x < MapData.MAP_WIDTH; x++)
            {
                MapTile tile = new MapTile($"x:{x}; y:{y}");
                tile.SetParent(this.transform);
                _mapTiles[x, y] = tile;
            }
        }
    }

    private void SetupMapMatrixGameObjects()
    {
        const float tileScale = 0.356f;

        int mapWidth = _mapTiles.GetLength(0);
        int mapHeight = _mapTiles.GetLength(1);

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                string name;
                Sprite tileSprite;

                switch ((TerrainType)_mapTiles[x, y].Value)
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

                _mapTiles[x, y].SetName(name);
                _mapTiles[x, y].SetMatrixIndex(x, y);
                _mapTiles[x, y].SetPosition(new Vector2(x * tileScale, y * tileScale));
                _mapTiles[x, y].SetSprite(tileSprite);
                _mapTiles[x, y].SetScale(new Vector2(13 * tileScale, 13 * tileScale));
            }
        }

        _gameManager.GameSettings.CurrentCity.MapData.SetMapBounds(CalculateBounds());
    }

    private Bounds CalculateBounds()
    {
        List<Renderer> renderers = new List<Renderer>(_mapTiles.Length);

        if (renderers.Count == 0)
            return new Bounds(transform.position, Vector3.zero);

        foreach (var tile in _mapTiles)
            renderers.Add(tile.GameObject.GetComponent<Renderer>());

        Bounds bounds = renderers[0].bounds;

        for (int i = 1; i < renderers.Count; i++)
            bounds.Encapsulate(renderers[i].bounds);

        return bounds;
    }
}
