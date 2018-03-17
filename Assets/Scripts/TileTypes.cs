using UnityEngine;
using System.Collections;

[System.Serializable]
public enum TileTypes
{
    Grass,
    Dirt,
    Rock,
    Water
}

[System.Serializable]
public enum LayoutType
{
    None,
    Wood,
    TrackHorizontal,
    TrackVertical,
    TrackLE,
    TrackLW,
    TrackNE,
    TrackNW,
    TrackCross,
    TrackStart,
    TrackEnd,

}