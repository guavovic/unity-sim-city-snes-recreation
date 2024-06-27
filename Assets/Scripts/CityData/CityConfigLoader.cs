using UnityEngine;

public sealed class CityConfigLoader : MonoBehaviour
{
    private KeyboardInputDisplayManager _keyboardInputDisplayManager;
    private CounterButtonsManager _counterManager;
    private MapPreviewDisplayManager _mapPreviewDisplayManager;

    public static CityConfigLoader Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        _keyboardInputDisplayManager = FindObjectOfType<KeyboardInputDisplayManager>();
        _counterManager = FindObjectOfType<CounterButtonsManager>();
        _mapPreviewDisplayManager = FindObjectOfType<MapPreviewDisplayManager>();
    }

    public void InitializeCityData()
    {
        // Debug.Log("Setando variaveis do mapa selecionado...");

        MapData selectedMap = new MapData(
             id: _counterManager.MapIndex,
             mapSprite: _mapPreviewDisplayManager.CurrentMapPreview,
             isIsland: false,
             mapTiles: new MapTile[MapData.MAP_WIDTH, MapData.MAP_HEIGHT]);

        Debug.Log(
            "As variaveis do mapa selecionado foram setadas! " +
            $"\n " +
            $"\n Map index: {selectedMap.Id}. " +
            $"\n Is island? {selectedMap.IsIsland}. " +
            $"\n Map lenght: ({selectedMap.MapMatrix.Length}) - [ Width={selectedMap.MapMatrix.GetLength(0)}; Height={selectedMap.MapMatrix.GetLength(1)}; ]. " +
            $"\n ");

        // Debug.Log("Gerando uma nova cidade...");

        CityModel newCity = new CityModel(
             name: _keyboardInputDisplayManager.CityName,
             mayorName: _keyboardInputDisplayManager.MayorName,
             mapData: new MapGenerate().GenerateMap(selectedMap));

        // Debug.Log("Nova cidade gerada!");

        Debug.Log(
            "Nova cidade gerada! " +
            $"\n " +
            $"\n City name: {newCity.Name}. " +
            $"\n Mayor name: {newCity.MayorName}. " +
            $"\n ");

        // Debug.Log("A cidade gerada foi setada.");

        GameManager.Instance.GameSettings.SetCurrentCity(newCity);
    }
}
