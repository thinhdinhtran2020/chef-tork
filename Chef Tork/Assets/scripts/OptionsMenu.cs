using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public GameObject MainMenuUI;
    public GameObject optionMenuUI;
    public void SetVolume (float volume)
    {

    }
    public void SaveOptions()
    {
        MainMenuUI.SetActive(true);
        optionMenuUI.SetActive(false);
    }
}
