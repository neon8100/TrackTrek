using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapAsset
{
    public LayoutType layout;
    public GameObject prefab;
}

[CreateAssetMenu(menuName = "Map Assets")]
public class MapAssets : ScriptableObject
{
    public MapAsset[] assets;

    public GameObject GetAsset(LayoutType type)
    {
        foreach(MapAsset asset in assets)
        {
            if(asset.layout == type)
            {
                return asset.prefab;
            }
        }

        return null;
    }


}
