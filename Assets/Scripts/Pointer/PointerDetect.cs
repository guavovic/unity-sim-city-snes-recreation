using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public sealed class PointerDetect : MonoBehaviour
{
    private Canvas _canvas;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _canvas = FindObjectOfType<Canvas>();
        _rectTransform = GetComponent<RectTransform>();
    }

    public void HandlePointerMovementInUI()
    {
        Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _canvas.transform as RectTransform,
            newPosition, 
            _canvas.worldCamera,
            out Vector2 anchoredPosition);

        _rectTransform.anchoredPosition = anchoredPosition;
    }
}
