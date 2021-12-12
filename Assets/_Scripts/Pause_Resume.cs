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

        GameObject go = GameObject.Find("TriggerLoadOnStart");

        if (go != null)
        {
            LoadGame();
            Destroy(go);
        }
    }

    // Update is called once per frame
    void Update()
    {
       if(GamePaused)
        {
            Time.timeScale = 0;
            //Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1;
            //Cursor.visible = false;
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
        
        // disable character controller to allow for setting the player position
        CharacterController cc = player.GetComponent<CharacterController>();
        if (cc != null)
        {
            cc.enabled = false;
        }

        if (pData != null)
        {
            player.transform.position = pData.GetPosition();
            player.transform.localRotation = pData.GetRotation();
        }
        else
        {
            Debug.Log("received null data object");
        }

        if (cc != null)
        {
            cc.enabled = true;
        }
    }
}
