using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour {

    public float speed = 1f;

    public void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.right, speed);
    }


    public void OnDestroy()
    {
        //GameEvents.events.
    }
}
