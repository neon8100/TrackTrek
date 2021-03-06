﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;

public class UIManager : MonoBehaviour {

    [SerializeField]
    private GameManager gameManager;

    private bool showPausePanel;
    private bool showTitlePanel;
    private bool showGameOverPanel;
    private bool pressPauseWhilePaused;

    [SerializeField]
    private EventSystem eventSystem;
    [SerializeField]
    private GameObject logoPanel;
    [SerializeField]
    private GameObject titleMenuPanel;
    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private GameObject youWinPanel;
    [SerializeField]
    private GameObject youLosePanel;
   
    [SerializeField]
    private Selectable startSelectable;
    [SerializeField]
    private Selectable pauseSelectable;
    [SerializeField]
    private Selectable gameOverSelectable;

    private void Awake()
    {
        GameEvents.Initialise();
    }

    // Use this for initialization
    void Start () {
        GameEvents.events.onGameStart += ShowTitlePanel;
        GameEvents.events.onGameOver += ShowGameOverPanel;
        GameEvents.events.onGameOver += ShowYouLosePanel;
        GameEvents.events.onGameWin += ShowYouWinPanel;
        GameEvents.events.onGameWin += ShowGameOverPanel;
        GameEvents.events.onGamePause += ShowPauseGameMenu;
        GameEvents.events.onGameRestart += ShowTitlePanel;
        GameEvents.events.onGameRestart += HideYouWinLosePanel;

        //startSelectable.Select();
	}

	public void ShowPauseGameMenu(){
        if (titleMenuPanel.activeInHierarchy || gameOverPanel.activeInHierarchy) { return; }
        
        if(!pausePanel.activeInHierarchy){
            showPausePanel = true;
        } else if (pausePanel.activeInHierarchy)
        {
            showPausePanel = false;
        }

        pausePanel.SetActive(showPausePanel);
        logoPanel.SetActive(showPausePanel);
        pauseSelectable.Select();
    }

    private void ShowGameOverPanel()
    {
        if (!gameOverPanel.activeInHierarchy)
        {
            gameOverPanel.SetActive(true);
            pausePanel.SetActive(false);
            titleMenuPanel.SetActive(false);
            
            gameOverSelectable.Select();
        } else if (gameOverPanel.activeInHierarchy){
            gameOverPanel.SetActive(false);
            pausePanel.SetActive(false);
            titleMenuPanel.SetActive(false);
            youLosePanel.SetActive(false);
        }
    }

    private void ShowTitlePanel()
    {
        if (!titleMenuPanel.activeInHierarchy)
        {
            titleMenuPanel.SetActive(true);
            pausePanel.SetActive(false);
            gameOverPanel.SetActive(false);
            logoPanel.SetActive(true);

            startSelectable.Select();
        }
        else if (titleMenuPanel.activeInHierarchy)
        {
            titleMenuPanel.SetActive(false);
            pausePanel.SetActive(false);
            gameOverPanel.SetActive(false);
            logoPanel.SetActive(false);
        }
    }

    private void ShowYouWinPanel(){
        youWinPanel.SetActive(true);
    }

    private void ShowYouLosePanel()
    {
        youLosePanel.SetActive(true);
    }

    private void HideYouWinLosePanel(){
        youLosePanel.SetActive(false);
        youWinPanel.SetActive(false);
    }

    public void RunStartGameEvent(){
        gameManager.LoadLevelScene();
        GameEvents.events.onGameStart();
    }

    public void RunPauseGameEvents()
    {
        GameEvents.events.onGamePause();
    }

    public void RunGameOverEvents()
    {
        GameEvents.events.onGameOver();
    }

    public void ReturnToTitle(){
        //GameEvents.events.onGamePause();
        GameEvents.events.onGameRestart();
    }

    public void RunQuitGame()
    {
        gameManager.QuitGame();
    }

	private void OnDestroy()
	{
        GameEvents.events.onGameStart -= ShowTitlePanel;
        GameEvents.events.onGamePause -= ShowPauseGameMenu;
        GameEvents.events.onGameOver -= ShowGameOverPanel;
        GameEvents.events.onGameRestart -= ShowTitlePanel;
        GameEvents.events.onGameWin -= ShowYouWinPanel;
        GameEvents.events.onGameWin -= ShowGameOverPanel;
        GameEvents.events.onGameOver -= ShowYouLosePanel;
        GameEvents.events.onGameRestart -= HideYouWinLosePanel;
    }

}
