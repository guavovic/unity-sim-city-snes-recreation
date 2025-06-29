using UnityEngine;

public class PointerManager : MonoBehaviour
{
    public PointerDetect PointerDetect;
    public LayerMask[] layersDetectMask;

    public CameraController CameraController;

    private void Update()
    {
        if (!GameStateController.IsBusy())
        {
            PointerDetect.HandlePointerMovementInUI();
            CheckScreenPointCollision();

            CameraController.HandleCameraMovement();
        }
    }

    private void CheckScreenPointCollision()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        foreach (var layer in layersDetectMask)
        {
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layer))
            {
                OnDetectTileHit(hit);
            }
        }
    }

    private void OnDetectTileHit(RaycastHit hit)
    {
        Debug.Log("Mouse está sobre o objeto: " + hit.collider.name);
    }
}
