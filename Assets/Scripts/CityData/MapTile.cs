using UnityEngine;

public sealed class MapTile
{
    public GameObject GameObject { get; private set; }
    public string Name { get; private set; }
    public string MatrixIndex { get; private set; }
    public char Value { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; }

    public void SetGameObject(GameObject gameObject) { GameObject = gameObject; }
    public void SetSpriteRenderer(SpriteRenderer spriteRenderer) { SpriteRenderer = spriteRenderer; }
    public void SetActive(bool active) { GameObject.SetActive(active); }
    public void SetPosition(Vector2 position) { GameObject.transform.position = position; }
    public void SetScale(Vector2 scale) { GameObject.transform.localScale = scale; }
    public void SetParent(Transform parent) { GameObject.transform.parent = parent; }
    public void SetSprite(Sprite sprite) { SpriteRenderer.sprite = sprite; }
    public void SetName(string name) { GameObject.name = Name = $"Tile {name} {MatrixIndex}"; }
    public void SetValue(char value) { Value = value; }
    public void SetMatrixIndex(int x, int y) { MatrixIndex = $"X={x}:Y={y}"; }
}