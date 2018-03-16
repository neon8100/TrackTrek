using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioAssets audioAssets;

    private AudioSource[] audioSource;
    private AudioClip audioClip;
    private AudioSource player1AudioSource;
    private AudioSource player2AudioSource;
    private AudioSource musicAudioSource;
    private AudioSource ambientAudioSource;

	private void Awake()
	{
        audioSource = GetComponents<AudioSource>();

        player1AudioSource = audioSource[0];
        player2AudioSource = audioSource[3];
        musicAudioSource = audioSource[2];
        ambientAudioSource = audioSource[1];
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
        GameEvents.events.onGamePause += PlayOnUITrackTypeSelect;

        //The game will start playing the ambience and music at the start and loop until you quit
        PlayAmbient();
        PlayMusic();
	}
	
    private void PlayAmbient(){
        ambientAudioSource.clip = audioAssets.GetAudioClip(AudioClipTypes.Ambience);
        ambientAudioSource.Play();
        ambientAudioSource.loop = true;
    }

    private void PlayMusic(){
        musicAudioSource.clip = audioAssets.GetAudioClip(AudioClipTypes.Music);
        musicAudioSource.Play();
        musicAudioSource.loop = true;
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
        GameEvents.events.onPickupResource -= PlayPickupResource;
        GameEvents.events.onDropItem -= PlayDropResource;
        GameEvents.events.onInteractResource -= PlayInteractWithResource;
        GameEvents.events.onCraftTrack -= PlayCraftTrack;
        GameEvents.events.onDeleteTrack -= PlayDeleteTrack;
        GameEvents.events.onTrainAboutToCrash -= PlayTrainAboutToCrash;
        GameEvents.events.onUIChooseTrack -= PlayOnUIChooseTrack;
        GameEvents.events.onGameWin -= PlayWin;
        GameEvents.events.onGameLose -= PlayLose;
        GameEvents.events.onResourceCreated -= PlayGenerateResource;
        GameEvents.events.onLayTrack -= PlayLayTrack;
        GameEvents.events.onUIOpenTrackSelect -= PlayOnUITrackTypeSelect;
    }

}
