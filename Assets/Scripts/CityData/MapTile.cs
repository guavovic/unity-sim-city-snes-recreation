using UnityEngine;

public class MapTile
{
    public GameObject GameObject { get; private set; }
    public string Name { get; private set; }
    public string MatrixIndex { get; private set; }
    public char Value { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; }

    public MapTile(string name)
    {
        Name = name;
        GameObject = new GameObject("Tile_" + Name);
        SpriteRenderer = GameObject.AddComponent<SpriteRenderer>();
        GameObject.SetActive(false);
    }

    public void SetActive(bool active) { GameObject.SetActive(active); }

    public void SetPosition(Vector2 position)
    {
        GameObject.transform.position = position;
    }

    public void SetScale(Vector2 scale)
    {
        GameObject.transform.localScale = scale;
    }

    public void SetParent(Transform parent)
    {
        GameObject.transform.parent = parent;
    }

    public void SetSprite(Sprite sprite)
    {
        SpriteRenderer.sprite = sprite;
    }

    public void SetName(string name)
    {
        Name = $"Tile {name} {MatrixIndex}";
        GameObject.name = Name;
    }

    public void SetValue(char value)
    {
        Value = value;
    }

    public void SetMatrixIndex(int x, int y)
    {
        MatrixIndex = $"(X={x}:Y={y})";
    }
}