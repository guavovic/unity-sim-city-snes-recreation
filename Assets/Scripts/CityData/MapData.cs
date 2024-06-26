using System.Collections.Generic;
using UnityEngine;

public class MapData
{
    public const int MAP_WIDTH = 120;
    public const int MAP_HEIGHT = 100;

    public static readonly Color32 LAND_COLOR = new Color32(181, 173, 140, 255);
    public static readonly Color32 FOREST_COLOR = new Color32(0, 66, 0, 255);
    public static readonly Color32 WATER_COLOR = new Color32(66, 132, 132, 255);

    private readonly Dictionary<TerrainType, int> _terrainAmounts = new Dictionary<TerrainType, int>
        {
            { TerrainType.Land, 0 },
            { TerrainType.Grass, 0 },
            { TerrainType.Forest, 0 },
            { TerrainType.Shore, 0 },
            { TerrainType.Water, 0 },
            { TerrainType.Unknown, 0 }
        };

    public int Id { get; private set; }
    public Sprite MapSprite { get; private set; }
    public Texture2D MapTexture { get; private set; }
    public bool IsIsland { get; private set; }
    public MapTile[,] MapMatrix { get; private set; }
    public Bounds MapBounds { get; private set; }
    public int LandAmount { get; private set; }
    public int GrassAmount { get; private set; }
    public int ForestAmount { get; private set; }
    public int ShoreAmount { get; private set; }
    public int WaterAmount { get; private set; }

    public MapData(int id, Sprite mapSprite, bool isIsland, MapTile[,] mapTiles)
    {
        Id = id;
        MapSprite = mapSprite;
        IsIsland = isIsland;
        MapMatrix = mapTiles;
    }

    public void SetId(int id)
    {
        Id = id;
    }

    public void SetMapSprite(Sprite mapSprite) { MapSprite = mapSprite; }

    public void SetMapBounds(Bounds mapBounds) { MapBounds = mapBounds; }

    public void SetMapTexture(Texture2D mapTexture) { MapTexture = mapTexture; }

    public void AddAmountTerrainType(TerrainType terrainType, int amount = 1)
    {
        if (_terrainAmounts.ContainsKey(terrainType))
            _terrainAmounts[terrainType] += amount;
    }

    public void RemoveAmountTerrainType(TerrainType terrainType, int amount = 1)
    {
        if (_terrainAmounts.ContainsKey(terrainType))
        {
            _terrainAmounts[terrainType] -= amount;

            if (_terrainAmounts[terrainType] < 0)
                _terrainAmounts[terrainType] = 0;
        }
    }
}