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

    public Sprite northSouth;
    public Sprite eastWest;

    public TravelDir direction = TravelDir.East;

    private void Start()
    {
        GameEvents.Initialise();
    }

    public void Update()
    {
        switch (direction)
        {
            case TravelDir.East:
                heading = transform.position + transform.right;
               gameObject.GetComponent<SpriteRenderer>().sprite = eastWest;
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
                break;
            case TravelDir.West:
                heading = transform.position - transform.right;
                gameObject.GetComponent<SpriteRenderer>().sprite = eastWest;
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                break;
            case TravelDir.North:
                heading = transform.position + transform.up;
                gameObject.GetComponent<SpriteRenderer>().sprite = northSouth;
                gameObject.GetComponent<SpriteRenderer>().flipY = false;
                break;
            case TravelDir.South:
                heading = transform.position - transform.up;
                gameObject.GetComponent<SpriteRenderer>().sprite = northSouth;
                gameObject.GetComponent<SpriteRenderer>().flipY = true;
                break;
        }
        
        transform.position = Vector3.MoveTowards(transform.position, heading, speed);

        currentTilePoint = new Vector3(Mathf.Floor(transform.position.x), Mathf.Floor(transform.position.y));

        CheckCurrentTrackDirection();

    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.GetComponent<TrackPiece>() != null)
            {
                currentTrack = collision.GetComponent<TrackPiece>();
            }
    }

    public TrackPiece currentTrack;
    public TrackPiece lastTrack;

    void CheckCurrentTrackDirection()
    {
        if (currentTrack != null)
        {
        
            if(Mathf.Floor(currentTrack.transform.position.x) == currentTilePoint.x && Mathf.Floor(currentTrack.transform.position.y) == currentTilePoint.y)
                {
                if (currentTrack != lastTrack)
                {

                    direction = currentTrack.GetOutputDirection(direction);
                    lastTrack = currentTrack;
                }
                    
                }
            
        }
    }
}
