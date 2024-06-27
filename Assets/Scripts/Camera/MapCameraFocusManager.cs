using UnityEngine;

public sealed class MapCameraFocusManager : MonoBehaviour
{
    private GameManager _gameManager;
    private Camera _mainCamera;

    public static MapCameraFocusManager Instance { get; private set; }

    private void Awake() { Instance = this; }

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _mainCamera = Camera.main;
    }

    public void CenterCameraOnMap()
    {
        Vector3 center = _gameManager.GameSettings.CurrentCity.MapData.MapBounds.center;
        _mainCamera.transform.position = new Vector3(center.x, center.y, Camera.main.transform.position.z);
    }

    public void CenterCameraOnRandomPosition()
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
