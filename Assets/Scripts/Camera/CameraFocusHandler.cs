using UnityEngine;

public sealed class CameraFocusHandler
{
    private readonly Camera _mainCamera;

    public CameraFocusHandler(Camera camera)
    {
        _mainCamera = camera;
    }

    public void CenterCameraOnPosition(Vector3 center)
    {
        _mainCamera.transform.position = new Vector3(center.x, center.y, _mainCamera.transform.position.z);
    }

    public void CenterCameraOnRandomPosition(Bounds bounds)
    {
        float cameraHeight = 2f * _mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * _mainCamera.aspect;

        float minX = bounds.min.x + cameraWidth / 2f;
        float maxX = bounds.max.x - cameraWidth / 2f;
        float minY = bounds.min.y + cameraHeight / 2f;
        float maxY = bounds.max.y - cameraHeight / 2f;

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        _mainCamera.transform.position = new Vector3(randomX, randomY, _mainCamera.transform.position.z);
    }
}
