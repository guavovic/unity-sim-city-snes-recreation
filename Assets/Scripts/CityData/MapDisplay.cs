using UnityEngine;

public sealed class MapDisplay : MonoBehaviour
{
    private GameManager _gameManager;
    private Camera _mainCamera;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _mainCamera = Camera.main;

        CenterCameraOnMap();
        CenterCameraOnRandomPosition();
        DisplayMap();
    }

    public void DisplayMap()
    {
        MapTile[,] mapTiles = _gameManager.GameSettings.CurrentCity.MapData.MapMatrix;

        foreach (var tile in mapTiles)
            tile.GameObject.SetActive(true);
    }

    private void CenterCameraOnMap()
    {
        Vector3 center = _gameManager.GameSettings.CurrentCity.MapData.MapBounds.center;
        _mainCamera.transform.position = new Vector3(center.x, center.y, Camera.main.transform.position.z);
    }

    private void CenterCameraOnRandomPosition()
    {
        Bounds mapBounds = _gameManager.GameSettings.CurrentCity.MapData.MapBounds;

        float cameraHeight = 2f * _mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * _mainCamera.aspect;

        float minX = mapBounds.min.x + cameraWidth / 2f;
        float maxX = mapBounds.max.x - cameraWidth / 2f;
        float minY = mapBounds.min.y + cameraHeight / 2f;
        float maxY = mapBounds.max.y - cameraHeight / 2f;

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        _mainCamera.transform.position = new Vector3(randomX, randomY, _mainCamera.transform.position.z);
    }
}
