using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrackConnection
{
    public TravelDir from;
    public TravelDir to;
}

public class TrackPiece : MonoBehaviour
{


    public bool isUnbuildable;

    public BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    [Tooltip("The connection point co-ordinates for this track piece.")]
    public TrackConnection[] connections;

    public TravelDir GetOutputDirection(TravelDir inputDirection)
    {

        foreach(TrackConnection connection in connections)
        {
            if(inputDirection == connection.from) { return connection.to; }
        }

        return TravelDir.None;

    }

    public bool isValidInputDirection(TravelDir inputDirection)
    {
        foreach (TrackConnection connection in connections)
        {
            if (inputDirection == connection.from) { return true; }
        }

        return false;
    }

}
