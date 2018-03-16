using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableMaterial : MonoBehaviour
{
    Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        GameEvents.events.onResourceCreated();
    }

    GameObject parent;

    public void Hold(GameObject character)
    {

        body.simulated = false;
        parent = character;
        GameEvents.events.onPickupResource();
    }

    public void Drop()
    {
        body.simulated = true;
        parent = null;
        GameEvents.events.onDropItem();
    }

    public void Update()
    {
        if (parent == null) { return; }
        else
        {
            transform.position = parent.transform.position;
        }
    }

}
