using UnityEngine;
using TMPro;

public class CounterButtonsManager : MonoBehaviour
{
    private const int MIN_VALUE = 0;
    private const int MAX_VALUE = 999;

    [SerializeField] private TMP_Text counterDisplay;

    private MapPreviewDisplayManager _previewDisplayManager;

    public int MapIndex { get; private set; }

    private void Awake()
    {
        _previewDisplayManager = FindObjectOfType<MapPreviewDisplayManager>();
    }

    private void Start()
    {
        UpdateCounterAndMapPreviewDisplay();
    }

    public void NextMapIndex()
    {
        MapIndex = (MapIndex + 1) % (MAX_VALUE + 1);
        UpdateCounterAndMapPreviewDisplay();
    }

    public void IncreaseUnit()
    {
        MapIndex = MapIndex < MAX_VALUE ? MapIndex + 1 : MIN_VALUE;
        UpdateCounterAndMapPreviewDisplay();
    }

    public void DecreaseUnit()
    {
        MapIndex = MapIndex > MIN_VALUE ? MapIndex - 1 : MAX_VALUE;
        UpdateCounterAndMapPreviewDisplay();
    }

    public void IncreaseTen()
    {
        MapIndex = MapIndex + 10 <= MAX_VALUE ? MapIndex += 10 : MapIndex %= 10;
        UpdateCounterAndMapPreviewDisplay();
    }

    public void DecreaseTen()
    {
        MapIndex = MapIndex - 10 >= MIN_VALUE ? MapIndex -= 10 : MAX_VALUE - (MAX_VALUE % 10) + (MapIndex % 10);
        UpdateCounterAndMapPreviewDisplay();
    }

    public void IncreaseHundred()
    {
        MapIndex = MapIndex + 100 <= MAX_VALUE ? MapIndex += 100 : MapIndex %= 100;
        UpdateCounterAndMapPreviewDisplay();
    }

    public void DecreaseHundred()
    {
        MapIndex = MapIndex - 100 >= MIN_VALUE ? MapIndex -= 100 : MAX_VALUE - (MAX_VALUE % 100) + (MapIndex % 100);
        UpdateCounterAndMapPreviewDisplay();
    }

    private void UpdateCounterAndMapPreviewDisplay()
    {
        if (MapIndex >= _previewDisplayManager.MapPreviewSprites.Length)
            MapIndex = 0;

        counterDisplay.text = MapIndex.ToString("D3");
        _previewDisplayManager.UpdateMapPreviewDisplay(MapIndex);
    }
}