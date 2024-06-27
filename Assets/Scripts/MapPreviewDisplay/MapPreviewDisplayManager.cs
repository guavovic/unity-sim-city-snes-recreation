using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public sealed class MapPreviewDisplayManager : MonoBehaviour
{
    [SerializeField] private Image mapPreviewImageReference;
    [SerializeField] private GameObject pleaseWaitImageReference;

    [SerializeField] private Sprite[] mapPreviewSprites;

    public Sprite CurrentMapPreview { get; private set; }

    public Sprite[] MapPreviewSprites { get { return mapPreviewSprites; } private set { mapPreviewSprites = value; } }

    private Coroutine _currentCoroutine;

    private void Awake()
    {
        MapPreviewSprites = Resources.LoadAll<Sprite>("Sprites/17188");
    }

    public void UpdateMapPreviewDisplay(int index)
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);

        _currentCoroutine = StartCoroutine(UpdateMapPreviewDisplayCoroutine(index));
    }

    private IEnumerator UpdateMapPreviewDisplayCoroutine(int index)
    {
        pleaseWaitImageReference.SetActive(true);
        mapPreviewImageReference.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.25f);

        CurrentMapPreview = mapPreviewSprites[index];
        mapPreviewImageReference.sprite = CurrentMapPreview;
        mapPreviewImageReference.gameObject.SetActive(true);
        pleaseWaitImageReference.SetActive(false);

        yield return null;
    }
}
