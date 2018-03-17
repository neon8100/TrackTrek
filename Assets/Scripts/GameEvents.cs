using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents
{
    public static GameEvents events;
    /// <summary>
    /// Initialised the GameEvent static member. Call this at the start if the app.
    /// </summary>
    /// 
    public static bool isInit;
    public static void Initialise()
    {
        if (isInit) { return; }
        events = new GameEvents();
        events.Init();
        isInit = true;
    }

    //Decalere a new delegate type
    public delegate void OnGameStart();
    public delegate void OnGameOver();
    public delegate void OnGamePause();
    public delegate void OnGameWin();
    public delegate void OnGameLose();
    public delegate void OnGameRestart();

    public delegate void OnPickupResource(PlayerController player);
    public delegate void OnDropResource(PlayerController player);
    public delegate void OnInteractResource();
    public delegate void OnResourceCreated();
    public delegate void OnDropItem();
    public delegate void OnCraftTrack(PlayerController player);
    public delegate void OnDeleteTrack(PlayerController player);
    public delegate void OnLayTrack(PlayerController player);
    public delegate void OnTrainAboutToCrash(PlayerController player);

    public delegate void OnUIChooseTrack(PlayerController player);
    public delegate void OnUIOpenTrackSelect();

    //Add a variable of the delegate type to be listened to when calling events.
    public OnGameStart onGameStart;
    public OnGameOver onGameOver;
    public OnGamePause onGamePause;
    public OnGameWin onGameWin;
    public OnGameLose onGameLose;
    public OnGameRestart onGameRestart;

    public OnPickupResource onPickupResource;
    public OnDropResource onDropResource;
    public OnInteractResource onInteractResource;
    public OnResourceCreated onResourceCreated;
    public OnDropItem onDropItem;
    public OnCraftTrack onCraftTrack;
    public OnDeleteTrack onDeleteTrack;
    public OnLayTrack onLayTrack;
    public OnTrainAboutToCrash onTrainAboutToCrash;

    public OnUIChooseTrack onUIChooseTrack;
    public OnUIOpenTrackSelect onUIOpenTrackSelect;

    /// <summary>
    /// Initialised the GameEvent delegates. Call this in the GameManager when a new game starts.
    /// </summary>
    public void Init()
    {
        events = this;

        onGameStart += () => { };
        onGameOver += () => { };
        onGamePause += () => { };
        onGameWin += () => { };
        onGameLose += () => { };

        onPickupResource += (x) => { };
        onDropResource += (x) => { };
        onInteractResource += () => { };
        onResourceCreated += () => { };
        onDropItem += () => { };
        onCraftTrack += (x) => { };
        onDeleteTrack += (x) => { };
        onLayTrack += (x) => { };
        onTrainAboutToCrash += (x) => { };

        onUIChooseTrack += (x) => { };
        onUIOpenTrackSelect += () => { };

    }

    /// <summary>
    /// Resets all the GameEvent delegates. Call this to reset the GameEvents.
    /// </summary>
    public void ClearAll()
    {
        
        onGameStart = null;
        onGameOver = null;
        onGamePause = null;
        onGameWin = null;
        onGameLose = null;

        onPickupResource = null;
        onDropItem = null;
        onDropResource = null;
        onInteractResource = null;
        onResourceCreated = null;
        onCraftTrack = null;
        onDeleteTrack = null;
        onLayTrack = null;
        onTrainAboutToCrash = null;

        onUIChooseTrack = null;
        onUIOpenTrackSelect = null;
    }
}
