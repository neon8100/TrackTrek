using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class UIManager : MonoBehaviour {

    public GameManager gameManager;

    private bool showPausePanel;
    private bool showTitlePanel;
    private bool showGameOverPanel;

    [SerializeField]
    private GameObject titleMenuPanel;
    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    private GameObject gameOverPanel;
   

	// Use this for initialization
	void Start () {
        GameEvents.events.onGameStart += ShowTitlePanel;
        GameEvents.events.onGameOver += ShowGameOverPanel;
        GameEvents.events.onGamePause += ShowPauseGameMenu;
	}
	
	// Update is called once per frame
	void Update () {
        if (showTitlePanel)
        {
            ShowTitlePanel();
            showTitlePanel = false;
        }

        if(showPausePanel){
            ShowPauseGameMenu();
            showPausePanel = false;
        }

        if (showGameOverPanel)
        {
            ShowGameOverPanel();
            showGameOverPanel = false;
        }



	}

	private void FixedUpdate()
	{
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            GameEvents.events.onGamePause();
        } 

        if(CrossPlatformInputManager.GetButtonDown("Fire2")){
            showTitlePanel = true;
        }

        if(CrossPlatformInputManager.GetButtonDown("Fire3")){
            showGameOverPanel = true;
        }
	}

    //Currently only activates the Pause Panel
    public void ShowPauseGameMenu(){
        if(!pausePanel.activeInHierarchy && !titleMenuPanel.activeInHierarchy && !gameOverPanel.activeInHierarchy){
            pausePanel.SetActive(true);
        } else if (pausePanel.activeInHierarchy){
            pausePanel.SetActive(false);
            titleMenuPanel.SetActive(false);
            gameOverPanel.SetActive(false);
        }

    }

    private void ShowGameOverPanel()
    {
        if (!gameOverPanel.activeInHierarchy)
        {
            gameOverPanel.SetActive(true);
            pausePanel.SetActive(false);
            titleMenuPanel.SetActive(false);
        } else if (gameOverPanel.activeInHierarchy){
            gameOverPanel.SetActive(false);
            pausePanel.SetActive(false);
            titleMenuPanel.SetActive(false);
        }
    }

    private void ShowTitlePanel()
    {
        if (!titleMenuPanel.activeInHierarchy)
        {
            titleMenuPanel.SetActive(true);
            pausePanel.SetActive(false);
            gameOverPanel.SetActive(false);
        }
        else if (titleMenuPanel.activeInHierarchy)
        {
            titleMenuPanel.SetActive(false);
            pausePanel.SetActive(false);
            gameOverPanel.SetActive(false);
        }
    }

	private void OnDestroy()
	{
        GameEvents.events.onGameStart -= ShowTitlePanel;
        GameEvents.events.onGamePause -= ShowPauseGameMenu;
        GameEvents.events.onGameOver -= ShowGameOverPanel;
	}

}
