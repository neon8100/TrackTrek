using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject gameSceneCamera;

    private bool isGamePaused;
    private bool shouldGameBePaused;

    public bool IsGamePaused { get; private set; } 
    public bool ShouldGameBePaused { get; private set; }

	private void Awake()
	{
        GameEvents.Initialise();


        if(gameObject.tag != "GameManager"){
            gameObject.tag = "GameManager";
        }
	}

	// Use this for initialization
	void Start () {
        GameEvents.events.onGameWin += SetTimeScale;
        GameEvents.events.onGamePause += SetTimeScale;
        GameEvents.events.onGameRestart += UnloadLevelScene;
        GameEvents.events.onGameRestart += ResetTimeScale;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (SceneManager.GetSceneByName("Level").isLoaded){
            gameSceneCamera.SetActive(false);
        } else if (!SceneManager.GetSceneByName("Level").isLoaded){
            gameSceneCamera.SetActive(true);
        }

        if (CrossPlatformInputManager.GetButtonDown("PauseP1") || CrossPlatformInputManager.GetButtonDown("PauseP2"))
        {
            GameEvents.events.onGamePause();
            //SetTimeScale();
        }
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            GameEvents.events.onGameWin();
            //SetTimeScale();
        }
    }

	public void StartGame(){
        GameEvents.events.onGameStart();
    }

    public void ResetGame(){
        Time.timeScale = 1;
        GameEvents.events.onGameStart();
    }

    public void ResetTimeScale()
    {
        Time.timeScale = 1;
    }


    public void SetTimeScale(){
        if (!isGamePaused)
        {
            if (Time.timeScale == 1f)
            {
                Time.timeScale = 0f;
            }
            
            isGamePaused = true;
        }
        else
        {
            if (Time.timeScale == 0f)
            {
                Time.timeScale = 1f;
            }
            isGamePaused = false;
        }
    }

    public void LoadLevelScene()
    {
        if (!SceneManager.GetSceneByName("Level").isLoaded)
        {
            AsyncOperation op = SceneManager.LoadSceneAsync("Level", LoadSceneMode.Additive);
            SceneManager.sceneLoaded += OnSceneLoaded;
         
        }
    }

    
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1){
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.SetActiveScene(arg0);
    }

    public void UnloadLevelScene()
    {
        Scene gameScene = SceneManager.GetSceneByName("Level");
        SceneManager.SetActiveScene(gameScene);
        SceneManager.UnloadSceneAsync(1);
    }

	private void OnDestroy()
	{
        //GameEvents.events.onGamePause -= SetTimeScale;
	}

	public void QuitGame(){
        Application.Quit();
    }

}
