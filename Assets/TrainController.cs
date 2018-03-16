using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TravelDir
{
    North,
    East,
    South,
    West,
    None
}

public class TrainController : MonoBehaviour {

    public float speed = 1f;

    List<TrackPiece> tracks;

    public Vector3 heading;
    public Vector3 currentTilePoint;

    public TravelDir direction = TravelDir.East;

    private void Start()
    {
        GameEvents.Initialise();
        GameEvents.events.onLayTrack += UpdateTrackList;

        UpdateTrackList();
    }

    public void Update()
    {
        switch (direction)
        {
            case TravelDir.East:
                heading = transform.position + transform.right;
                break;
            case TravelDir.West:
                heading = transform.position - transform.right;
                break;
            case TravelDir.North:
                heading = transform.position + transform.up;
                break;
            case TravelDir.South:
                heading = transform.position - transform.up;
                break;
        }
        
        transform.position = Vector3.MoveTowards(transform.position, heading, speed);
        currentTilePoint = new Vector3(Mathf.Floor(transform.position.x), Mathf.Floor(transform.position.y));

        CheckCurrentTrackDirection();

    }


    public void OnTriggerStay2D(Collider2D collision)
    {
            if (collision.GetComponent<TrackPiece>() != null)
            {
                currentTrack = collision.GetComponent<TrackPiece>();
            }
        
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        currentTrack = null;
    }

    public TrackPiece currentTrack;
    public TrackPiece lastTrack;

    void CheckCurrentTrackDirection()
    {
        if (currentTrack != null)
        {
           if (currentTrack != lastTrack)
            {
                Debug.Log("MATCH");
                currentTrack.GetOutputDirection(direction);
                lastTrack = currentTrack;
            }
        }
    }

    void UpdateTrackList()
    {
        tracks = LevelGenerator.instance.tracks;
    }
}
