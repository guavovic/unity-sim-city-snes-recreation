using GV.Extensions;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public sealed class MapPreviewDisplayManager : MonoBehaviour
{
    [SerializeField] private Image mapPreviewImageReference;
    [SerializeField] private GameObject pleaseWaitImageReference;

    [ReadOnly][SerializeField] private Sprite[] mapPreviewSprites; 

    public Sprite CurrentMapPreview { get; private set; }

    public Sprite[] MapPreviewSprites { get { return mapPreviewSprites; } private set { mapPreviewSprites = value; } }

    private Coroutine _currentCoroutine;

    private void Awake()
    {
        // Remover isso daqui
        // Criar um objeto para guardar todos esses valores
        MapPreviewSprites = Resources.LoadAll<Sprite>("Sprites/MapPreviews");
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
