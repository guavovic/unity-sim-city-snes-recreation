using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public sealed class MapGenerate
{
    // Revisar laços que estao gerando o mapa, talvez eu fiz uma logica confusa de preencher 

    public MapData GenerateMap(MapData selectedMap)
    {
        // Debug.Log("Gerando mapa...");

        // Debug.Log($"Map matrix lenght: [{selectedMap.MapMatrix.Length}]");

        selectedMap.SetMapTexture(SpriteToTexture2D(selectedMap.MapSprite));

        // Debug.Log("Textura do mapa a ser analisada foi setada!");

        AnalyzeMapTexture(selectedMap);

        // Debug.Log("Geração do mapa completa!");

        return selectedMap;
    }

    private Texture2D SpriteToTexture2D(Sprite sprite)
    {
        var texture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
        Color[] pixels = sprite.texture.GetPixels((int)sprite.textureRect.x, (int)sprite.textureRect.y, (int)sprite.textureRect.width, (int)sprite.textureRect.height);
        Color32[] pixels32 = Array.ConvertAll(pixels, c => (Color32)c);
        texture.SetPixels32(pixels32);
        texture.Apply();
        return texture;
    }

    private void AnalyzeMapTexture(MapData mapData)
    {
        // Debug.Log("Analisando a textura do mapa!");

        Color32[] pixels = mapData.MapTexture.GetPixels32();

        Dictionary<Color32, TerrainType> colorTerrainMapping = new Dictionary<Color32, TerrainType>
        {
            { MapData.LAND_COLOR, TerrainType.Land },
            { MapData.FOREST_COLOR, TerrainType.Forest },
            { MapData.WATER_COLOR, TerrainType.Water }
        };

        int width = mapData.MapTexture.width;
        int height = mapData.MapTexture.height;

        // Debug.Log($"Texture resolution: [ X={width}; Y={height}; ]");

        for (int pixelY = 0; pixelY < height; pixelY++)
        {
            for (int pixelX = 0; pixelX < width; pixelX++)
            {
                AnalyzePixel(mapData, pixels, pixelX, pixelY, width, colorTerrainMapping);
            }
        }
    }

    private void AnalyzePixel(MapData mapData, Color32[] pixels, int pixelX, int pixelY, int width, Dictionary<Color32, TerrainType> colorTerrainMapping)
    {
        // Debug.Log($"Pixel position: X={pixelX}; Y={pixelY}; ]");

        int index = pixelY * width + pixelX;

        // Debug.Log($"Pixel index: [{index}]");

        Color32 pixelColor = pixels[index];

        TerrainType terrainType = colorTerrainMapping.FirstOrDefault(pair => IsColorSimilar(pixelColor, pair.Key)).Value;

        // Debug.Log($"Terrain type found: {(char)terrainType}");

        mapData.AddAmountTerrainType(terrainType);

        mapData.MapMatrix[pixelX, pixelY].SetValue((char)terrainType);

        // Debug.Log($"Value in map matrix: [{mapData.MapMatrix[pixelX, pixelY]}]");
    }

    private bool IsColorSimilar(Color32 a, Color32 b, int tolerance = 50)
    {
        int toleranceSquared = tolerance * tolerance;

        int diffR = a.r - b.r;
        int diffG = a.g - b.g;
        int diffB = a.b - b.b;

        return (diffR * diffR + diffG * diffG + diffB * diffB) < 3 * toleranceSquared;
    }
}
