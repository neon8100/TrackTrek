using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TileAsset
{
    public TileTypes tileType;
    //Sprite for this tile
    //We can extend this to a list later
    public GameObject tile;
}

[CreateAssetMenu(menuName ="Tiles/Tile Assets")]
public class TileAssets : ScriptableObject
{
    public List<TileAsset> tileAssets;

    public GameObject GetTile(TileTypes tileType)
    {
        foreach(TileAsset asset in tileAssets)
        {
            if(asset.tileType == tileType)
            {
                return asset.tile;
            }
        }

        return null;
    }

}
