using UnityEngine;

public static class ScriptableObjectPaths
{
    public const string MapPreviewSpriteCollectionsPath = "ScriptableObjects/MapPreviewSpriteCollection";
    public const string TerrainSpritesPath = "ScriptableObjects/TerrainSprites";

    public static string GetPath<T>() where T : ScriptableObject
    {
        return typeof(T) switch
        {
            { } t when t == typeof(MapPreviewSpriteCollection) => MapPreviewSpriteCollectionsPath,
            { } t when t == typeof(TerrainSprites) => TerrainSpritesPath,
            _ => throw new System.NotImplementedException(),
        };
    }
};