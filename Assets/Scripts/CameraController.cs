using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float boundaryBuffer = 1f;

    [SerializeField] private float minOrthographicSize = 2f;
    [SerializeField] private float maxOrthographicSize = 10f;

    private Camera mainCamera;
    private float cameraHalfHeight;
    private float cameraHalfWidth;
    private Vector2 mapMinBounds;
    private Vector2 mapMaxBounds;

    public void Initialize()
    {
        mainCamera = Camera.main;
        cameraHalfHeight = mainCamera.orthographicSize;
        cameraHalfWidth = cameraHalfHeight * mainCamera.aspect;

        var mapData = GameManager.Instance.GameSettings.CurrentCity.MapData;

        mapMinBounds = new Vector2(mapData.MapBounds.min.x - boundaryBuffer, mapData.MapBounds.min.y - boundaryBuffer);
        mapMaxBounds = new Vector2(mapData.MapBounds.max.x + boundaryBuffer, mapData.MapBounds.max.y + boundaryBuffer);
    }

    private void Update()
    {
        if (mainCamera)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0f).normalized;
            Vector3 newPosition = transform.position + moveSpeed * Time.deltaTime * moveDirection;

            float clampedX = Mathf.Clamp(newPosition.x, mapMinBounds.x + cameraHalfWidth, mapMaxBounds.x - cameraHalfWidth);
            float clampedY = Mathf.Clamp(newPosition.y, mapMinBounds.y + cameraHalfHeight, mapMaxBounds.y - cameraHalfHeight);

            transform.position = new Vector3(clampedX, clampedY, transform.position.z);

            // Ajustar o tamanho ortográfico da câmera com scroll do mouse ou teclas Q e E
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");

            if (scrollInput != 0f)
            {
                float newSize = mainCamera.orthographicSize - scrollInput;
                mainCamera.orthographicSize = Mathf.Clamp(newSize, minOrthographicSize, maxOrthographicSize);
            }
        }
    }
}
