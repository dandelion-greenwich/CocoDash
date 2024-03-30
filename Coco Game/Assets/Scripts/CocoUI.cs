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
    public TextMeshProUGUI treatsCollectedCounter;
    public TextMeshProUGUI treatsLeftCounter, timerCountdown;

    public enum GameState {MainMenu, Pause, Active, Victory, Loss}
    public GameState currentState;
    public GameObject pauseMenuPanel, allGameUI, mainMenu, gameOver;
    public static bool GameIsPaused = false;

    private void Awake()
    {
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
                break;
            case GameState.Active:
                /*Resume();*/ // stops stack overflow error - D'Arcy
                Time.timeScale = 1f;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                break;
            case GameState.Victory:
                Victory();
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                break;
            case GameState.Loss:
                Loss();
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
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

    /*    public void GamePaused()
        {
            pauseMenuPanel.SetActive(true);
            allGameUI.SetActive(true);
            gameOver.SetActive(false);
            mainMenu.SetActive(false);
        }

        public void LoadMainMenu()
        {
            pauseMenuPanel.SetActive(false);
            allGameUI.SetActive(false);
            gameOver.SetActive(false);
            mainMenu.SetActive(true);
        }

        public void ResumeGame()
        {
            pauseMenuPanel.SetActive(false);
            allGameUI.SetActive(true);
            gameOver.SetActive(false);
            mainMenu.SetActive(false);
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

    }
    public void Loss()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
