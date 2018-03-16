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
    BoxCollider2D boxCollider;

    [Tooltip("The connection point co-ordinates for this track piece.")]
    public TrackConnection[] connections;

    private void Awake()
    {
        boxCollider = gameObject.AddComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
    }

    public TravelDir GetOutputDirection(TravelDir inputDirection)
    {

        foreach(TrackConnection connection in connections)
        {
            if(connection.from == inputDirection) { return connection.to; }
            if(connection.to == inputDirection) { return connection.from; }
        }

        return TravelDir.None;

    }

}
