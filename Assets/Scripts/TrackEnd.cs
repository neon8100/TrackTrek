using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackEnd : MonoBehaviour {

    private void OnEnable()
    {
        gameObject.name = "END";
    }
    public void OnTriggerEnter2D(Collider2D collision)
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
