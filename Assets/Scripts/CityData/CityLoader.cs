using UnityEngine;

public sealed class CityLoader : MonoBehaviour
{
    private KeyboardInputDisplayManager _keyboardInputDisplayManager;
    private CounterButtonsManager _counterManager;
    private MapPreviewDisplayManager _mapPreviewDisplayManager;

    private void Awake()
    {
        _keyboardInputDisplayManager = FindObjectOfType<KeyboardInputDisplayManager>();
        _counterManager = FindObjectOfType<CounterButtonsManager>();
        _mapPreviewDisplayManager = FindObjectOfType<MapPreviewDisplayManager>();
    }

    public void Load()
    {
        MapData newMapData = new MapData(
            id: _counterManager.MapIndex,
            mapSprite: _mapPreviewDisplayManager.CurrentMapPreview,
            isIsland: false,
            mapTiles: new MapTile[MapData.MAP_WIDTH, MapData.MAP_HEIGHT]);

        CityModel newCity = new CityModel(
             name: _keyboardInputDisplayManager.CityName,
             mayorName: _keyboardInputDisplayManager.MayorName,
             mapData: new MapGenerate().GenerateMap(newMapData));

        GameManager.Instance.GameSettings.SetCurrentCity(newCity);
    }
}
