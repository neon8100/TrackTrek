﻿using System.Collections;
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
    public bool isEngine;
    public GameObject explosionPrefab;
    List<TrackPiece> tracks;

    public Vector3 heading;
    public Vector3 currentTilePoint;

    public Sprite northSouth;
    public Sprite eastWest;

    public TravelDir direction = TravelDir.East;

    public int maxTime = 5;
    int gameOverTime;

    public void OnEnable()
    {
        gameObject.name = "Train";
    }

    private void Start()
    {
        //GameEvents.Initialise();

        gameOverTime = maxTime;

        StartCoroutine(Count());
    }

    public void Update()
    {
        switch (direction)
        {
            case TravelDir.East:
                heading = transform.position + transform.right;
               gameObject.GetComponent<SpriteRenderer>().sprite = eastWest;
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
                gameObject.GetComponent<SpriteRenderer>().flipY = false;
                break;
            case TravelDir.West:
                heading = transform.position - transform.right;
                gameObject.GetComponent<SpriteRenderer>().sprite = eastWest;
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                gameObject.GetComponent<SpriteRenderer>().flipY = false;
                break;
            case TravelDir.North:
                heading = transform.position + transform.up;
                gameObject.GetComponent<SpriteRenderer>().sprite = northSouth;
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                gameObject.GetComponent<SpriteRenderer>().flipY = false;
                break;
            case TravelDir.South:
                heading = transform.position - transform.up;
                gameObject.GetComponent<SpriteRenderer>().sprite = northSouth;
                gameObject.GetComponent<SpriteRenderer>().flipY = true;
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                break;
        }
        
        transform.position = Vector3.MoveTowards(transform.position, heading, speed * Time.timeScale);

        currentTilePoint = new Vector3(Mathf.Floor(transform.position.x), Mathf.Floor(transform.position.y));

        CheckCurrentTrackDirection();

    }

    IEnumerator Count()
    {
        yield return new WaitForSeconds(1f);

        gameOverTime--;
        if (gameOverTime < 0)
        {
            Crash();
        }
        else
        {
            StartCoroutine(Count());
        }
    }

    public void Crash()
    {
        GameObject explo = Instantiate(explosionPrefab);
        explo.transform.position = transform.position;

        GameEvents.events.onGameLose();

        if (isEngine)
        {
            GameEvents.events.onGameOver();
        }

        Destroy(gameObject);
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
       
            if (collision.GetComponent<TrackPiece>() != null)
            {
                gameOverTime = maxTime;
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
                    if (currentTrack.isValidInputDirection(direction))
                    {
                        direction = currentTrack.GetOutputDirection(direction);
                        lastTrack = currentTrack;
                    }
                    else
                    {
                        Crash();
                    }
                }
                    
                }
            
        }
    }
}
