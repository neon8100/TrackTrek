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
        GameEvents.events.onPickupResource += PlayPickupResource;
        GameEvents.events.onDropItem += PlayDropResource;
        GameEvents.events.onInteractResource += PlayInteractWithResource;
        GameEvents.events.onCraftTrack += PlayCraftTrack;
        GameEvents.events.onDeleteTrack += PlayDeleteTrack;
        GameEvents.events.onTrainAboutToCrash += PlayTrainAboutToCrash;
        GameEvents.events.onUIChooseTrack += PlayOnUIChooseTrack;
        GameEvents.events.onGameWin += PlayWin;
        GameEvents.events.onGameLose += PlayLose;
        GameEvents.events.onResourceCreated += PlayGenerateResource;
        GameEvents.events.onLayTrack += PlayLayTrack;
        GameEvents.events.onUIOpenTrackSelect += PlayOnUITrackTypeSelect;

        //The game will start playing the ambience and music at the start and loop until you quit
        PlayAmbient();
        PlayMusic();
	}
	
    private void PlayAmbient(){
        audioSource[1].clip = audioAssets.GetAudioClip(AudioClipTypes.Ambience);
        audioSource[1].Play();
        audioSource[1].loop = true;
    }

    private void PlayMusic(){
        audioSource[2].clip = audioAssets.GetAudioClip(AudioClipTypes.Music);
        audioSource[2].Play();
        audioSource[2].loop = true;
    }

    private void PlayPickupResource(){
        audioSource[0].clip = audioAssets.GetAudioClip(AudioClipTypes.PickUpItem);
        audioSource[0].Play();
    }

    private void PlayDropResource(){
        audioSource[0].clip = audioAssets.GetAudioClip(AudioClipTypes.DropItem);
        audioSource[0].Play();
    }

    private void PlayInteractWithResource(){
        //Only plays mine sound right now
        audioSource[0].clip = audioAssets.GetAudioClip(AudioClipTypes.MineChop);
        audioSource[0].Play();
    }

    private void PlayGenerateResource()
    {
        audioSource[0].clip = audioAssets.GetAudioClip(AudioClipTypes.TreeFall);
        audioSource[0].Play();
    }

    private void PlayCraftTrack(){
        audioSource[0].clip = audioAssets.GetAudioClip(AudioClipTypes.CraftTrack);
        audioSource[0].Play();
    }

    private void PlayDeleteTrack()
    {
        audioSource[0].clip = audioAssets.GetAudioClip(AudioClipTypes.DeleteTrack);
        audioSource[0].Play();
    }

    private void PlayLayTrack()
    {
        audioSource[0].clip = audioAssets.GetAudioClip(AudioClipTypes.LayTrack);
        audioSource[0].Play();
    }

    private void PlayTrainAboutToCrash()
    {
        audioSource[0].clip = audioAssets.GetAudioClip(AudioClipTypes.TrainAboutToCrash);
        audioSource[0].Play();
    }

    private void PlayOnUIChooseTrack()
    {
        audioSource[0].clip = audioAssets.GetAudioClip(AudioClipTypes.ChooseTrackType);
        audioSource[0].Play();
    }

    private void PlayOnUITrackTypeSelect()
    {
        audioSource[0].clip = audioAssets.GetAudioClip(AudioClipTypes.SelectTypeUI);
        audioSource[0].Play();
    }

    private void PlayWin()
    {
        audioSource[0].clip = audioAssets.GetAudioClip(AudioClipTypes.Win);
        audioSource[0].Play();
    }

    private void PlayLose()
    {
        audioSource[0].clip = audioAssets.GetAudioClip(AudioClipTypes.Lose);
        audioSource[0].Play();
    }

    private void OnDestroy()
    {
       
    }

}
