using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackEnd : MonoBehaviour {
    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<TrainController>())
        {
            if (collision.gameObject.GetComponent<TrainController>().direction == TravelDir.East)
            {
                GameEvents.events.onGameWin();
            }
        }
    }
}
