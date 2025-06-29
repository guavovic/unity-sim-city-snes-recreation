using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTileGameObject : MonoBehaviour
{
    public MapTile Tile { get; private set; }

    public void SetTile(MapTile tile) { Tile = tile; }
}
