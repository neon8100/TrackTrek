using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioAssets audioAssets;

    private AudioSource[] audioSource;
    private AudioClip audioClip;

	private void Awake()
	{
        audioSource = GetComponents<AudioSource>();
	}

	// Use this for initialization
	void Start () {
        GameEvents.events.onPickupResource += PlayPickupResourceSound;
        GameEvents.events.onGameStart += PlayAmbient;
	}
	
    private void PlayAmbient(){
        audioSource[1].clip = audioAssets.GetAudioClip(AudioClipTypes.Ambience);
        audioSource[1].Play();
    }

    private void PlayPickupResourceSound(){
        //Only plays wood sound right now
        audioSource[0].clip = audioAssets.GetAudioClip(AudioClipTypes.TreeChop);
        audioSource[0].Play();
    }


}
