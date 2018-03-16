using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour {

    SpriteRenderer spriteRenderer;

    public int framesPerSecond = 15;

    int _currentFrame;
    public int currentFrame { get { return _currentFrame; } }

    public Sprite[] sprites;

    private Sprite[] selectedFrames;
    public void SetSelectedFrames(Sprite[] frames)
    {
        selectedFrames = frames;
    }
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        selectedFrames = sprites;
    }

    float count;
    private void FixedUpdate()
    {
        if (stopped) return;
        float secondsPerFrame = (float)framesPerSecond/60;
        count += secondsPerFrame;
        if (count >= 1)
        {
            count = 0;
            UpdateFrame();
        }

    }

    public void UpdateFrame()
    {
        if (currentFrame > selectedFrames.Length - 1) { _currentFrame = 0; }
        spriteRenderer.sprite = selectedFrames[currentFrame];
        _currentFrame++;

    }

    bool stopped;
    public void Stop()
    {
        stopped = true;
        spriteRenderer.sprite = selectedFrames[0];
    }

    public void Start()
    {
        stopped = false;
    }
}
