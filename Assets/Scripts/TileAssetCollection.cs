using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Tile Asset Collection")]
public class TileAssetCollection : ScriptableObject
{

    public List<TileAsset> tileAssets;

    public GameObject GetTile(TileTypes tileType)
    {
        foreach (TileAsset asset in tileAssets)
        {
            if (asset.tileType == tileType)
            {
                return asset.tile;
            }
        }

        return null;
    }
}
