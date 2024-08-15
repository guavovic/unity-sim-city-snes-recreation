using UnityEngine;

public sealed class MapTile
{
    public GameObject GameObject { get; private set; }
    public string Name { get; private set; }
    public string MatrixIndex { get; private set; }
    public char Value { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; }

    public void AssignGameObject(GameObject gameObject) { GameObject = gameObject; }
    public void AssignSpriteRenderer(SpriteRenderer spriteRenderer) { SpriteRenderer = spriteRenderer; }
    public void SetVisibility(bool isVisible) { GameObject.SetActive(isVisible); }
    public void SetPosition(Vector2 position) { GameObject.transform.position = position; }
    public void SetScale(Vector2 scale) { GameObject.transform.localScale = scale; }
    public void SetParent(Transform parent) { GameObject.transform.parent = parent; }
    public void AssignSprite(Sprite sprite) { SpriteRenderer.sprite = sprite; }
    public void UpdateName(string name) { GameObject.name = Name = $"Tile {name} - {MatrixIndex}"; }
    public void UpdateValue(char value) { Value = value; }
    public void UpdateMatrixIndex(int x, int y) { MatrixIndex = $"{x}:{y}"; }
}