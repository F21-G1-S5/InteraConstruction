using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Provides basic functions for the main menu, including scene-switching
/// with or without a save file and exiting the application.
/// </summary>
public class MenuHandler : MonoBehaviour
{

    public void LOAD_SCENE(string SceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneName);
    }

    public void LoadFromSave(string sceneName)
    {
        GameObject go = new GameObject("TriggerLoadOnStart");
        Object.DontDestroyOnLoad(go);

        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
