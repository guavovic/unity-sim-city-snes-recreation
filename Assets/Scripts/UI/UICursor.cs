using UnityEngine;
using UnityEngine.UI;

public sealed class UICursor : MonoBehaviour
{
    public RectTransform RectTransform { get; private set; }
    public Image Image { get; private set; }

    private void Awake()
    {
        // RectTransform = GetComponent<RectTransform>();
        //Image = GetComponent<Image>();
    }

    public void SetPosition(Vector2 newPosition)
    {
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }
}