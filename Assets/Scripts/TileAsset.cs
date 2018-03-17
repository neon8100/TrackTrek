using UnityEngine;
using System.Collections;

[System.Serializable]
public class TileAsset
{
    public TileTypes tileType;
    //Sprite for this tile
    //We can extend this to a list later
    public GameObject tile;
}
