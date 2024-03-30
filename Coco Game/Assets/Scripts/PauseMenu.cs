using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    /*public static bool GameIsPaused = false;
    public GameObject pauseMenuPanel, allGameUI, mainMenu, gameOver;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true; // working pause menu that shows canvas and working buttons when 'esc' key is pressed - D'Arcy
    }

*//*    public void LoadMainMenu()
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
}
