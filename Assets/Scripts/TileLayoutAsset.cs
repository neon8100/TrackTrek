using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level/TileLayout")]
public class TileLayoutAsset : ScriptableObject
{
    public TileAssets tileAssetGroup;
    public Vector2 mapSize;
    public List<TileTypes> tiles;

    public List<GameObject> GetTiles()
    {
        List<GameObject> tileList = new List<GameObject>();

        foreach (TileTypes tileType in tiles)
        {
            tileList.Add(tileAssetGroup.GetTile(tileType));
        }

        return tileList;

    }
    
}
