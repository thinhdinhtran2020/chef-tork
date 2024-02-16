using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;
    public int Apples;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Apples = GameManager.Instance.Apples;
            SaveVariable(Apples, "Apples");
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }


    public static void SaveVariable(int value, string variableName)
    {
        PlayerPrefs.SetInt(variableName, value);
        PlayerPrefs.Save();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public static int LoadVariable(string variableName, int defaultValue = 0)
    {
        if (PlayerPrefs.HasKey(variableName))
        {
            return PlayerPrefs.GetInt(variableName);
        }
        else
        {
            return defaultValue;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int loadedValue = LoadVariable("Apples", 0);
        Debug.Log("Loaded Value: " + loadedValue);
        GameManager.Instance.Apples = loadedValue;
    }

}
