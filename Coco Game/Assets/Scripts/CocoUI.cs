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

    // for mechanics ui, need two different images layered on top of each other, so when one is not usuable, the other ui image shows

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
        CheckInputs();
        Debug.Log(currentState);
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
                break;
            case GameState.Pause:
                Pause();
                Time.timeScale = 0f;
                break;
            case GameState.Active:
                Resume();
                Time.timeScale = 1f;
                break;
            case GameState.Victory:
                Victory();
                Time.timeScale = 0f;
                break;
            case GameState.Loss:
                Loss();
                Time.timeScale = 0f;
                break;
        }
    }
    public void CheckInputs()
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
    }

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
        CheckGameState(GameState.Active);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        CheckGameState(GameState.MainMenu);
    }
    public void Pause()
    {
        /*CheckGameState(GameState.Pause);*/
    }
    public void Resume()
    {
        /*CheckGameState(GameState.Active);*/
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
