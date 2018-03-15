using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PressToPause : MonoBehaviour {

    private bool pressedPauseButton;
    [SerializeField]
    private GameObject pausePanel;

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        if(pressedPauseButton){
            PauseGame();
            pressedPauseButton = false;
        }
	}

	private void FixedUpdate()
	{
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            pressedPauseButton = true;
        }
	}

    //Currently only activates the Pause Panel
    private void PauseGame(){
        if(!pausePanel.activeInHierarchy){
            pausePanel.SetActive(true);
        } else if (pausePanel.activeInHierarchy){
            pausePanel.SetActive(false);
        }

    }

}
