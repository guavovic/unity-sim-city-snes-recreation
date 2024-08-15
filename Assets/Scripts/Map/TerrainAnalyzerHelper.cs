using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class TerrainAnalyzerHelper
{
    public static Texture2D ConvertSpriteToTexture(Sprite sprite)
    {
        var texture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
        Color[] pixels = sprite.texture.GetPixels((int)sprite.textureRect.x, (int)sprite.textureRect.y, (int)sprite.textureRect.width, (int)sprite.textureRect.height);
        Color32[] pixels32 = Array.ConvertAll(pixels, c => (Color32)c);
        texture.SetPixels32(pixels32);
        texture.Apply();
        return texture;
    }

    public static void AnalyzeTexture(MapInfo mapInfo)
    {
        Color32[] pixels = mapInfo.MapTexture.GetPixels32();

        var colorToTerrainMap = new Dictionary<Color32, TerrainType>
        {
            { MapConstants.LAND_COLOR, TerrainType.Land },
            { MapConstants.FOREST_COLOR, TerrainType.Forest },
            { MapConstants.WATER_COLOR, TerrainType.Water }
        };

        int width = mapInfo.MapTexture.width;
        int height = mapInfo.MapTexture.height;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                AnalyzeAndAssignPixel(mapInfo, pixels, x, y, width, colorToTerrainMap);
            }
        }
    }

    private static void AnalyzeAndAssignPixel(MapInfo mapInfo, Color32[] pixels, int x, int y, int width, Dictionary<Color32, TerrainType> colorToTerrainMap)
    {
        int pixelIndex = y * width + x;
        Color32 pixelColor = pixels[pixelIndex];

        TerrainType terrainType = colorToTerrainMap.FirstOrDefault(pair => IsColorMatch(pixelColor, pair.Key)).Value;

        mapInfo.TerrainData.AddAmountTerrainType(terrainType);
        mapInfo.MapGrid.Tiles[x, y].UpdateValue((char)terrainType);
    }

    private static bool IsColorMatch(Color32 color1, Color32 color2, int tolerance = 50)
    {
        int toleranceSquared = tolerance * tolerance;

        int redDifference = color1.r - color2.r;
        int greenDifference = color1.g - color2.g;
        int blueDifference = color1.b - color2.b;

        return (redDifference * redDifference + greenDifference * greenDifference + blueDifference * blueDifference) < 3 * toleranceSquared;
    }
}
