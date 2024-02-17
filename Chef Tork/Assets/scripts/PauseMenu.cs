using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject optionMenuUI;
    public GameObject savedGameText;


    // Update is called once per frame
    void Update()
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

    void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void SaveGame()
    {
        savedGameText.SetActive(true);
        Invoke("RemoveSavedGameText", 3f);
    }

    void RemoveSavedGameText()
    {
        savedGameText.SetActive(false);
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; 
        GameIsPaused = true;

    }

    public void Options()
    {
        pauseMenuUI.SetActive(false);
        optionMenuUI.SetActive(true);
    }

    public void Resume()
    {
        RemoveSavedGameText();
        ResumeGame();
    }

    public void Save()
    {
        SaveGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}

