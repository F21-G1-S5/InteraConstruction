using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>SampleUse</c> serves as an example for how to use the SaveSystem class to save and load data
/// attaching this MonoBehaviour to an object will use its gameObject component to save its position and rotation
/// </summary>
public class SampleUse : MonoBehaviour
{
    // Update is called once per frame. Use the S and L keys to trigger the Save and Load functions respectively
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveData();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadData();
        }
    }

    private void SaveData()
    {
        JSONSaveSystem.SavePlayer(gameObject, "exampleData");
    }

    private void LoadData()
    {
        PlayerData pData = JSONSaveSystem.LoadPlayer("exampleData");

        if (pData != null)
        {
            gameObject.transform.position = pData.GetPosition();
            gameObject.transform.localRotation = pData.GetRotation();
        }
        else
        {
            Debug.Log("received null data object");
        }
    }
}