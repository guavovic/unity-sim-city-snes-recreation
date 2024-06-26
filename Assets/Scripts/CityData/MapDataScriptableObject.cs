using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "Game/Map Data", order = 1)]
public sealed class MapResourcesScriptableObject : ScriptableObject
{
    public Sprite[] mapPreviewSprites;

    public void LoadSprites(string path)
    {
        //mapSprites = Resources.LoadAll<Sprite>(path);
    }


}
