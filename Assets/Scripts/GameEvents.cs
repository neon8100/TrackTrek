using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents
{
    public static GameEvents events;
    /// <summary>
    /// Initialised the GameEvent static member. Call this at the start if the app.
    /// </summary>
    public static void Initialise()
    {
        events = new GameEvents();
        events.Init();
    }

    //Decalere a new delegate type
    public delegate void OnGameStart();
    public delegate void OnGameOver();
    public delegate void OnGamePause();
    public delegate void OnGameWin();
    public delegate void OnGameLose();
    public delegate void OnGameRestart();

    public delegate void OnPickupResource();
    public delegate void OnDropResource();
    public delegate void OnInteractResource();
    public delegate void OnResourceCreated();
    public delegate void OnDropItem();
    public delegate void OnCraftTrack();
    public delegate void OnDeleteTrack();
    public delegate void OnLayTrack();
    public delegate void OnTrainAboutToCrash();

    public delegate void OnUIChooseTrack();
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

        onPickupResource += () => { };
        onDropResource += () => { };
        onInteractResource += () => { };
        onResourceCreated += () => { };
        onDropItem += () => { };
        onCraftTrack += () => { };
        onDeleteTrack += () => { };
        onLayTrack += () => { };
        onTrainAboutToCrash += () => { };

        onUIChooseTrack += () => { };
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
