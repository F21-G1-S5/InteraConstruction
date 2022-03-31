using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Provides methods to the in-game pause screen
/// </summary>
public class Pause_Resume : MonoBehaviour
{
    public GameObject player;

    public GameObject targetMenu;
    public bool isGamePausable = true;
    public bool usePlayFabSave = true;
    public static bool gamePaused = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
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
        ActivatePauseMenu();
    }

    //Handle pause and resume for ESC key
    private void ActivatePauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(gamePaused)
            {
                targetMenu.SetActive(false);
                Time.timeScale = 1.0f;
                Cursor.visible = false;
                gamePaused = false;
                Debug.Log("Pause menu inactive");
            }
            else
            {
                targetMenu.SetActive(true);
                Cursor.visible = true;
                if (isGamePausable) Time.timeScale = 0.0f;
                gamePaused = true;
                Debug.Log("Pause menu active");
            }
        }

    }

    //Handle resume game for UI button
    public void ResumeGame()
    {
        targetMenu.SetActive(false);
        Time.timeScale = 1.0f;
        Cursor.visible = false;
        gamePaused = false;
    }

    public void SaveGame()
    {
        if (player == null)
        {
            return;
        }

        if (usePlayFabSave)
        {
            PlayFabDataManager.SaveUserData(player);
        }
        else
        {
            JSONSaveSystem.SavePlayer(player, "savefile1");
        }
    }

    public void LoadGame()
    {
        if (player == null)
        {
            return;
        }

        PlayerData pData;
        if (usePlayFabSave)
        {
            PlayFabDataManager.LoadUserData(
                (result) =>
                {
                    pData = result;
                    Debug.Log("moving player");
                    Debug.Log(pData);
                    MovePlayer(pData);
                });
        }
        else
        {
            pData = JSONSaveSystem.LoadPlayer("savefile1");

            MovePlayer(pData);
        }
    }

    private void MovePlayer(PlayerData pData)
    {
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

    public void LOAD_SCENE(string SceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneName);
    }
}
