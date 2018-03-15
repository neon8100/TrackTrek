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

    //Add a variable of the delegate type to be listened to when calling events.
    public OnGameStart onGameStart;
    public OnGameOver onGameOver;
    public OnGamePause onGamePause;

    /// <summary>
    /// Initialised the GameEvent delegates. Call this in the GameManager when a new game starts.
    /// </summary>
    public void Init()
    {
        events = this;
    }

    /// <summary>
    /// Resets all the GameEvent delegates. Call this to reset the GameEvents.
    /// </summary>
    public void ClearAll()
    {
        
        onGameStart = null;
        onGameOver = null;
        onGamePause = null;
    }


}
