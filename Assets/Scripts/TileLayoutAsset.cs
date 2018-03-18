using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level/TileLayout")]
public class TileLayoutAsset : ScriptableObject
{
    public TileAssetCollection tileAssetGroup;
    public Vector2 mapSize;
    [HideInInspector()]
    public TileTypes[] tiles;
    [HideInInspector()]
    public LayoutType[] layouts;

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
