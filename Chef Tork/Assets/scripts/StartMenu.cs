using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject MainMenuUI;
    public GameObject optionMenuUI;

    public void OptionsMenu()
    {
        MainMenuUI.SetActive(false);
        optionMenuUI.SetActive(true);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
