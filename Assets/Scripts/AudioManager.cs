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
    private AudioSource nonPlayerSFXSource;

	private void Awake()
	{
        audioSource = GetComponents<AudioSource>();

        player1AudioSource = audioSource[0];
        player2AudioSource = audioSource[1];
        musicAudioSource = audioSource[2];
        ambientAudioSource = audioSource[3];
        nonPlayerSFXSource = audioSource[4];
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
        GameEvents.events.onGameStart += PlayMusic;
        GameEvents.events.onGameRestart += StopMusic;


        //The game will start playing the ambience and music at the start and loop until you quit
        PlayAmbient();

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

    private void StopMusic()
    {
        musicAudioSource.clip = audioAssets.GetAudioClip(AudioClipTypes.Music);
        musicAudioSource.Stop();
    }

    private void PlayPickupResource(PlayerController player){
        audioSource[(int)player.playerNumber].clip = audioAssets.GetAudioClip(AudioClipTypes.PickUpItem);
        audioSource[(int)player.playerNumber].Play();
    }

    private void PlayDropResource(){
        audioSource[4].clip = audioAssets.GetAudioClip(AudioClipTypes.DropItem);
        audioSource[4].Play();
    }

    private void PlayInteractWithResource(){
        //Only plays mine sound right now
        audioSource[4].clip = audioAssets.GetAudioClip(AudioClipTypes.TreeChop);
        audioSource[4].Play();
    }

    private void PlayGenerateResource()
    {
        audioSource[4].clip = audioAssets.GetAudioClip(AudioClipTypes.TreeFall);
        audioSource[4].Play();
    }

    private void PlayCraftTrack(PlayerController player){
        audioSource[(int)player.playerNumber].clip = audioAssets.GetAudioClip(AudioClipTypes.CraftTrack);
        audioSource[(int)player.playerNumber].Play();
    }

    private void PlayDeleteTrack(PlayerController player)
    {
        audioSource[(int)player.playerNumber].clip = audioAssets.GetAudioClip(AudioClipTypes.DeleteTrack);
        audioSource[(int)player.playerNumber].Play();
    }

    private void PlayLayTrack(PlayerController player)
    {
        audioSource[(int)player.playerNumber].clip = audioAssets.GetAudioClip(AudioClipTypes.LayTrack);
        audioSource[(int)player.playerNumber].Play();
    }

    private void PlayTrainAboutToCrash(PlayerController player)
    {
        audioSource[(int)player.playerNumber].clip = audioAssets.GetAudioClip(AudioClipTypes.TrainAboutToCrash);
        audioSource[(int)player.playerNumber].Play();
    }

    private void PlayOnUIChooseTrack(PlayerController player)
    {
        audioSource[(int)player.playerNumber].clip = audioAssets.GetAudioClip(AudioClipTypes.ChooseTrackType);
        audioSource[(int)player.playerNumber].Play();
    }

    private void PlayOnUITrackTypeSelect()
    {
        audioSource[4].clip = audioAssets.GetAudioClip(AudioClipTypes.SelectTypeUI);
        audioSource[4].Play();
    }

    private void PlayWin()
    {
        audioSource[4].clip = audioAssets.GetAudioClip(AudioClipTypes.Win);
        audioSource[4].Play();
    }

    private void PlayLose()
    {
        audioSource[4].clip = audioAssets.GetAudioClip(AudioClipTypes.Lose);
        audioSource[4].Play();
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
        GameEvents.events.onGameStart += PlayMusic;
    }

}
