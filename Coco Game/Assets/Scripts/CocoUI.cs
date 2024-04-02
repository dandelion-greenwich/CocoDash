using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CocoUI : MonoBehaviour
{
    public float countdown;
    int minutes, seconds;
    public TextMeshProUGUI treatsCollectedCounter, treatsLeftCounter, timerCountdown;
    AudioSource audioSourceCamera;
    public enum GameState {MainMenu, Pause, Active, Victory, Loss, Replay}
    public GameState currentState;
    public static bool GameIsPaused = false;
    public GameObject firstObjective, secondObjective, cameraMusic, pauseMenuPanel, allGameUI, mainMenu, victoryPanel, gameOverPanel;

    private void Awake()
    {
        firstObjective.SetActive(true);
        secondObjective.SetActive(false);
        audioSourceCamera = cameraMusic.GetComponent<AudioSource>();
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            CheckGameState(GameState.MainMenu);
        }
        else
        {
            CheckGameState(GameState.Active);
        }
    }

    public void UpdateTreats()
    {
        treatsCollectedCounter.text = GameManager.treatsCollected.ToString(); //added UI to increase treat counter - D'Arcy
        treatsLeftCounter.text = GameManager.treatsLeft.ToString();
    }
    private void Update()
    {
        treatsCollectedCounter.text = GameManager.treatsCollected.ToString(); //added UI to increase treat counter - D'Arcy
        treatsLeftCounter.text = GameManager.treatsLeft.ToString();
        Timer();
        /*CheckInputs();*/
        //Debug.Log(currentState);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
                pauseMenuPanel.SetActive(false);
                GameIsPaused = false;
            }
            else
            {
                Pause();
                pauseMenuPanel.SetActive(true);
                GameIsPaused = true; // slightly changed pause game state to work with the pause menu panel - D'Arcy
            }
        }
    }
    public void Timer()
    {
        if (countdown > 0)
        {
            countdown -= Time.deltaTime;
        }
        else if (countdown <= 0)
        {
            countdown = 0;
        }
        minutes = Mathf.FloorToInt(countdown / 60);
        seconds = Mathf.FloorToInt(countdown % 60);
        timerCountdown.text = string.Format("{0:00}:{1:00}", minutes, seconds); //Has a bug when after reaching 00:00 for a split second it shows 1
    }
    //To Do: make a game state check

    public void CheckGameState(GameState newGameState)
    {
        currentState = newGameState;
        switch (currentState)
        {
            case GameState.MainMenu:
                MainMenu();
                Cursor.lockState = CursorLockMode.None;
                break;
            case GameState.Pause:
                /*Pause();*/ // stops stack overflow error - D'Arcy
                Time.timeScale = 0f;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                audioSourceCamera.Pause();
                break;
            case GameState.Active:
                /*Resume();*/ // stops stack overflow error - D'Arcy
                Time.timeScale = 1f;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                audioSourceCamera.Play();
                break;
            case GameState.Victory:
                Victory();
                Time.timeScale = 0f;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                audioSourceCamera.Pause();
                firstObjective.SetActive(false);
                secondObjective.SetActive(false);
                break;
            case GameState.Loss:
                Loss();
                Time.timeScale = 0f;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                audioSourceCamera.Pause();
                firstObjective.SetActive(false);
                secondObjective.SetActive(false);
                break;
            case GameState.Replay:
                Replay();
                Time.timeScale = 0f;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                break;
        }
    }
    /*    public void CheckInputs()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (currentState == GameState.Active)
                {
                    CheckGameState(GameState.Pause);
                }
                else if (currentState == GameState.Pause)
                {
                    CheckGameState(GameState.Active);
                }
            }
        }*/

    public void Active()
    {
        SceneManager.LoadScene("MapLevel");
        CheckGameState(GameState.Active); // active game state when game is running the level - D'Arcy
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        CheckGameState(GameState.MainMenu); // working 'main menu' button in pause menu - D'Arcy
    }
    public void Pause()
    {
        CheckGameState(GameState.Pause);
        pauseMenuPanel.SetActive(true);
        GameIsPaused = true; // brings up pause menu when player pauses game - D'Arcy
    }
    public void Resume()
    {
        CheckGameState(GameState.Active);
        pauseMenuPanel.SetActive(false);
        GameIsPaused = false; // resumes game from pause menu - D'Arcy
    }
    public void Victory()
    {
        CheckGameState(GameState.Pause);
        victoryPanel.SetActive(true);
        GameIsPaused = true; // shows victory panel when player completes level - D'Arcy
    }
    public void Loss()
    {
        CheckGameState(GameState.Pause);
        gameOverPanel.SetActive(true);
        GameIsPaused = true; // shows game over panel when player fails level - D'Arcy
    }

    public void Replay()
    {
        CheckGameState(GameState.Active);
        gameOverPanel.SetActive(false);
        GameIsPaused = false; // restarts the game when the player presses the 'replay' button - D'Arcy
        /*SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);*/
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void ChangeObjective()
    {
        firstObjective.SetActive(false);
        secondObjective.SetActive(true);
    }
}
