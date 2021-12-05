using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDbuldozer : MonoBehaviour
{

    public GameObject buldozerTDScreen;
    public GameObject PauseButton;
    public GameObject player;


    public static bool TDGamePaused;

    // Start is called before the first frame update
    void Start()
    {
        TDGamePaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (TDGamePaused)
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
        Pause_Resume.GamePaused = true;
        TDGamePaused = true;

        buldozerTDScreen.SetActive(true);

        PauseButton.SetActive(false);

    }

    public void ResumeGame()
    {
        TDGamePaused = false;
        Pause_Resume.GamePaused = false;
        buldozerTDScreen.SetActive(false);
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
