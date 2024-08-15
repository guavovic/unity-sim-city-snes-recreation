using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A utility class for loading and caching ScriptableObjects from Resources.
/// </summary>
/// <remarks>
/// The ScriptableObjectLoader provides a generic method to load instances of ScriptableObjects
/// from a specified path within the Resources folder. The loaded objects are cached to avoid
/// redundant loads, improving performance. If the requested ScriptableObject is not found, an
/// error is logged to assist with debugging.
/// </remarks>
public static class ScriptableObjectLoader
{
    private static readonly Dictionary<string, ScriptableObject> _loadedScriptableObjects = new Dictionary<string, ScriptableObject>();

    /// <summary>
    /// Loads a ScriptableObject of the specified type from the given path.
    /// </summary>
    /// <typeparam name="T">The type of ScriptableObject to load. Must be a subclass of ScriptableObject.</typeparam>
    /// <param name="path">The path to the ScriptableObject within the Resources folder.</param>
    /// <returns>The loaded ScriptableObject of type T, or null if not found.</returns>
    /// <remarks>
    /// If the ScriptableObject is already loaded and cached, it is returned from the cache.
    /// If it is not cached, it will be loaded from the Resources folder and added to the cache.
    /// An error is logged if the ScriptableObject could not be found at the specified path.
    /// </remarks>
    public static T Load<T>(string path) where T : ScriptableObject
    {
        if (_loadedScriptableObjects.ContainsKey(path))
            return _loadedScriptableObjects[path] as T;

        T scriptableObject = Resources.Load<T>(path);

        if (scriptableObject == null)
        {
            Debug.LogError($"ScriptableObject of type {typeof(T)} not found at path: {path}");
            return null;
        }

        _loadedScriptableObjects[path] = scriptableObject;
        return scriptableObject;
    }

    public static T Load<T>() where T : ScriptableObject
    {
        string path = ScriptableObjectPaths.GetPath<T>();

        if (string.IsNullOrEmpty(path))
            return null;

        return Load<T>(path);
    }
}