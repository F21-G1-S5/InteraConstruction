using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_Resume : MonoBehaviour
{
    public GameObject PauseScreen;
    public GameObject PauseButton;
    public GameObject player;

    
    public static bool GamePaused;

    // Start is called before the first frame update
    void Start()
    {
        GamePaused = false;
    }

    // Update is called once per frame
    void Update()
    {
       if(GamePaused)
        {
            Time.timeScale = 0;
            
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void PauseGame()
    {
        GamePaused = true;

        PauseScreen.SetActive(true);

        PauseButton.SetActive(false);

    }

    public void ResumeGame()
    {
        GamePaused = false;
        PauseScreen.SetActive(false);
        PauseButton.SetActive(true);
    }

    public void SaveGame()
    {
        JSONSaveSystem.SavePlayer(player, "savefile1");
    }

    public void LoadGame()
    {
        PlayerData pData = JSONSaveSystem.LoadPlayer("savefile1");

        if (pData != null)
        {
            player.transform.position = pData.GetPosition();
            player.transform.localRotation = pData.GetRotation();
        }
        else
        {
            Debug.Log("received null data object");
        }
    }
}