﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum PlayerID
{
    Player1,
    Player2
}

public class PlayerController : MonoBehaviour {

    public PlayerID playerNumber;
    int playerId;
    public float lookAheadSpeed;
    public float playerSpeed;

    Transform lookTarget;

    private void Awake()
    {
        GameObject t = new GameObject();
        lookTarget = t.transform;

        playerId = (int)playerNumber + 1;

        foreach (string s in Input.GetJoystickNames())
        {
            Debug.Log(s);
        }


        onActionButtonDown = new UnityEvent();
        onActionButtonUp = new UnityEvent();
        onActionButtonStay = new UnityEvent();

        onActionButtonDown.AddListener(CheckActionInputs);



    }

    private void Update()
    {
        
        float vertical = Input.GetAxis("VerticalP"+playerId);
        float horizontal = Input.GetAxis("HorizontalP"+playerId);

        Vector2 pos = new Vector2(lookTarget.position.x, lookTarget.position.y);
        Vector2 newPos = pos + new Vector2(horizontal * lookAheadSpeed, vertical * lookAheadSpeed);

        lookTarget.position = newPos;

        transform.up = lookTarget.position;
        transform.position = Vector3.Lerp(transform.position, lookTarget.position, playerSpeed);

        
    }

    private void LateUpdate()
    {
        //Button Event Listeners
        if (actionButton > 0)
        {
            if (!actionButtonIsDown)
            {
                onActionButtonDown.Invoke();
            }

            onActionButtonStay.Invoke();

            actionButtonIsDown = true;
        }
        else
        {
            onActionButtonUp.Invoke();
            actionButtonIsDown = false;
        }
    }


    //Checks collisions and then determines what to try and do
    public void OnCollisionStay2D(Collision2D collision)
    {
        this.collision = collision;
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        this.collision = null;
    }

    bool holdingMaterial;

    public Collision2D collision;
    public CollectableMaterial material;

    void HandleMaterialCollision(CollectableMaterial material)
    {
        if (actionButton > 0 && this.material!=material)
        {
            material.Hold(gameObject);
            this.material = material;
            holdingMaterial = true;
            
        }
    }

    void CheckActionInputs()
    {
        if (holdingMaterial)
        {
            material.Drop();
            holdingMaterial = false;
            this.material = null;
        }
        else
        {
            if (collision.gameObject.GetComponent<CollectableMaterial>())
            {
                //Handles a collision against a material object
                HandleMaterialCollision(collision.gameObject.GetComponent<CollectableMaterial>());
            }
        }
    }

    /*
    void CheckButtonPresses()
    {
        if (actionButton>0 && !actionButtonIsDown)
        {
            onActionButtonDown.Invoke();
            
        }
    }
    */

    float actionButton { get { return Input.GetAxis("FireP" + playerId); } }

    bool actionButtonIsDown;

    UnityEvent onActionButtonDown;
    UnityEvent onActionButtonStay;
    UnityEvent onActionButtonUp;
}
