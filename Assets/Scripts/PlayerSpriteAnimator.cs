using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpriteAnimator : SpriteAnimator
{
    protected override void Awake()
    {
        base.Awake();
        state = State.Idle;
        SetSelectedFrames(idle);
     
    }
    public enum State
    {
        Idle,
        Moving,
        Action
    }

    public State state;

    public Sprite[] idle;
    public Sprite[] moving;
    public Sprite[] action;

    public void SwitchState(State state)
    {
        SwitchState(state, false);
    }

    public void SwitchState(State state, bool animationLock)
    {
        if(this.state == state) { return; }

        if (animLock && !completedOnce)
        {
            StartCoroutine(WaitForAnimLock(state, animationLock));
            return;
        }

        this.state = state;
        animLock = animationLock;
        

        switch (this.state)
        {
            case State.Action:
                SetSelectedFrames(action);
                break;
            case State.Idle:
                SetSelectedFrames(idle);
                break;
            case State.Moving:
                SetSelectedFrames(moving);
                break;

        }
        
    }

    IEnumerator WaitForAnimLock(State state, bool animationLock)
    {
        yield return new WaitForEndOfFrame();
        SwitchState(state, animationLock);

    }







}
