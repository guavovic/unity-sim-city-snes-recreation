using UnityEngine;

public sealed class CameraController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    [Header("Camera Movement")]
    [SerializeField] private float moveSpeed = 7.5f;
    [SerializeField] private float moveSmoothTime = 0.1f;
    [SerializeField] private float borderMargin = 3f;
    [SerializeField] private float edgeMoveThreshold = 25f;

    private Vector2 _mapMinBounds;
    private Vector2 _mapMaxBounds;

    private float _cameraHalfWidth;
    private float _cameraHalfHeight;

    [Header("Camera Zoom")]
    [SerializeField] private float minOrthographicSize = 2f;
    [SerializeField] private float maxOrthographicSize = 17f;
    [SerializeField] private float zoomSpeed = 10f;
    [SerializeField] private float zoomSensitivity = 5f;

    private float _targetOrthographicSize;

    private Vector3 _targetPosition;
    private Vector3 _velocity = Vector3.zero;

    private bool _cameraInitialized = false;

    private CameraFocusHandler _cameraFocusController;

    public static CameraController Instance { get; private set; }

    private void Awake() { Instance = this; }

    public void Initialize()
    {
        if (mainCamera == null)
        {
            Debug.LogWarning("main Camera not seted.");
            return;
        }

        _cameraFocusController ??= new CameraFocusHandler(mainCamera);

        _targetOrthographicSize = mainCamera.orthographicSize;
        _targetPosition = mainCamera.transform.position;
        _targetPosition.z = mainCamera.transform.position.z;

        var mapInfo = GameManager.Instance.GameSettings.City.MapInfo;

        _mapMinBounds = new Vector2(mapInfo.Bounds.min.x, mapInfo.Bounds.min.y);
        _mapMaxBounds = new Vector2(mapInfo.Bounds.max.x, mapInfo.Bounds.max.y);

        _cameraFocusController.CenterCameraOnPosition(mapInfo.Bounds.center);

        _cameraInitialized = true;
    }

    private void Update()
    {
        if (_cameraInitialized && !GameStateController.IsBusy())
        {
            HandleCameraMovement();
            HandleCameraZoom();
            SmoothCameraPosition();
        }
    }

    private void HandleCameraMovement()
    {
        float speedMultiplier = _targetOrthographicSize / mainCamera.orthographicSize;
        Vector3 newPosition = _targetPosition + moveSpeed * speedMultiplier * Time.deltaTime * CalculateMovementDirection();

        ClampAndSetTargetPosition(newPosition);
    }

    private Vector3 CalculateMovementDirection()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0f).normalized;
        Vector3 mousePosition = Input.mousePosition;

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        if (mousePosition.x < edgeMoveThreshold)
        {
            moveDirection += Vector3.left;
        }
        else if (mousePosition.x > screenWidth - edgeMoveThreshold)
        {
            moveDirection += Vector3.right;
        }

        if (mousePosition.y < edgeMoveThreshold)
        {
            moveDirection += Vector3.down;
        }
        else if (mousePosition.y > screenHeight - edgeMoveThreshold)
        {
            moveDirection += Vector3.up;
        }

        return moveDirection;
    }

    private void ClampAndSetTargetPosition(Vector3 newPosition)
    {
        float clampedX = Mathf.Clamp(newPosition.x, _mapMinBounds.x + _cameraHalfWidth - borderMargin, _mapMaxBounds.x - _cameraHalfWidth + borderMargin);
        float clampedY = Mathf.Clamp(newPosition.y, _mapMinBounds.y + _cameraHalfHeight - borderMargin, _mapMaxBounds.y - _cameraHalfHeight + borderMargin);

        _targetPosition = new Vector3(clampedX, clampedY, _targetPosition.z);
    }

    private void HandleCameraZoom()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;

        if (scrollInput == 0f)
        {
            if (Input.GetKey(KeyCode.Plus) || Input.GetKey(KeyCode.KeypadPlus))
            {
                scrollInput = -zoomSensitivity;
            }
            else if (Input.GetKey(KeyCode.Minus) || Input.GetKey(KeyCode.KeypadMinus))
            {
                scrollInput = zoomSensitivity;
            }
        }

        if (scrollInput != 0f)
        {
            UpdateTargetOrthographicSize(scrollInput);

            if (scrollInput > 0)
                FocusOnMousePosition();
        }

        ApplyZoom();
    }

    private void UpdateTargetOrthographicSize(float scrollInput)
    {
        _targetOrthographicSize -= scrollInput;
        _targetOrthographicSize = Mathf.Clamp(_targetOrthographicSize, minOrthographicSize, maxOrthographicSize);
    }

    private void FocusOnMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCamera.transform.position.z;
        Vector3 worldMousePosition = mainCamera.ScreenToWorldPoint(mousePosition);

        float speedMultiplier = _targetOrthographicSize / mainCamera.orthographicSize;
        _targetPosition = worldMousePosition - (worldMousePosition - _targetPosition) * speedMultiplier;
        _targetPosition.z = mainCamera.transform.position.z;

        ClampAndSetTargetPosition(_targetPosition);
    }

    private void ApplyZoom()
    {
        if (mainCamera.orthographicSize != _targetOrthographicSize)
        {
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, _targetOrthographicSize, zoomSpeed * Time.deltaTime);
            _cameraHalfHeight = mainCamera.orthographicSize;
            _cameraHalfWidth = _cameraHalfHeight * mainCamera.aspect;
            ClampAndSetTargetPosition(_targetPosition);
        }
    }

    private void SmoothCameraPosition()
    {
        mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, _targetPosition, ref _velocity, moveSmoothTime);
    }
}