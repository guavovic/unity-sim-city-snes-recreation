using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public sealed class MapPreviewDisplayManager : MonoBehaviour
{
    [SerializeField] private Image mapPreviewImageReference;
    [SerializeField] private GameObject pleaseWaitImageReference;

    private Coroutine _currentCoroutine;
    private GameSettings _gameSettings;

    public Sprite[] MapPreviewSprites { get; private set; }
    public static Sprite CurrentMapPreview { get; private set; }

    private void Awake()
    {
        MapPreviewSprites = ScriptableObjectLoader.Load<MapPreviewSpriteCollection>().PreviewSprites;
    }

    private void Start()
    {
        _gameSettings = GameManager.Instance.GameSettings;
    }

    public void UpdateMapPreviewDisplay(int index)
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);

        _currentCoroutine = StartCoroutine(UpdateMapPreviewDisplayCoroutine(index));
    }

    public void SaveCurrentInfos()
    {
        if (_currentCoroutine != null)
            return;

        _gameSettings.City.MapInfo.SetPreviewSprite(CurrentMapPreview);
    }

    private IEnumerator UpdateMapPreviewDisplayCoroutine(int index)
    {
        pleaseWaitImageReference.SetActive(true);
        mapPreviewImageReference.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.25f);

        CurrentMapPreview = MapPreviewSprites[index];
        mapPreviewImageReference.sprite = CurrentMapPreview;
        mapPreviewImageReference.gameObject.SetActive(true);
        pleaseWaitImageReference.SetActive(false);

        yield return null;
    }
}
