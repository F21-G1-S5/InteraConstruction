using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDcrane : MonoBehaviour
{
    public GameObject craneTDScreen;
    //public GameObject PauseButton;
    public GameObject player;
    public GameObject minimap0;
    public GameObject minimap1;
    public GameObject minimap2;
    public GameObject minimap3;
    public GameObject mainPauseButton0;
    public GameObject mainPauseButton1;
    public GameObject mainPauseButton2;
    public GameObject mainPauseButton3;
    public GameObject promptedTD;


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
        

        minimap0.SetActive(false);
        minimap1.SetActive(false);
        minimap2.SetActive(false);
        minimap3.SetActive(false);
        mainPauseButton0.SetActive(false);
        mainPauseButton1.SetActive(false);
        mainPauseButton2.SetActive(false);
        mainPauseButton3.SetActive(false);

        promptedTD.SetActive(false);


        craneTDScreen.SetActive(true);

        //PauseButton.SetActive(false);
        Pause_Resume.GamePaused = true;
        TDGamePaused = true;

    }

    public void ResumeGame()
    {
        minimap0.SetActive(true);
        minimap1.SetActive(true);
        minimap2.SetActive(true);
        minimap3.SetActive(true);
        mainPauseButton0.SetActive(true);
        mainPauseButton1.SetActive(true);
        mainPauseButton2.SetActive(true);
        mainPauseButton3.SetActive(true);

        TDGamePaused = false;
        Pause_Resume.GamePaused = false;
        craneTDScreen.SetActive(false);
        //PauseButton.SetActive(true);
        
        mainPauseButton1.SetActive(true);
        mainPauseButton2.SetActive(true);
        mainPauseButton3.SetActive(true);
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
