using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

/// <summary>
/// The PlayFabDataManager holds user data such as the user type and save data,
/// and provides methods for saving and retrieving this data from PlayFab
/// </summary>
public static class PlayFabDataManager
{
    private static string userType = "Trainee";
    private static PlayerData cachedData; 

    public static string GetUserType()
    {
        return userType;
    }

    public static void SaveUserData(PlayerData pData)
    {
        string position = ArrayToCSV(pData.position);
        string rotation = ArrayToCSV(pData.rotation);
        Debug.Log(position);

        var userTypeRequest = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { "userType", userType },
                { "position", position },
                { "rotation", rotation }
            }
        };
        PlayFabClientAPI.UpdateUserData(userTypeRequest,
            (UpdateUserDataResult result) => { Debug.Log("Sucessfully save player data!"); },
            OnError);
    }

    public static void SaveUserData(GameObject go)
    {
        PlayerData pData = new PlayerData(go);
        SaveUserData(pData);
    }

    // load use data and cache it for later use
    public static void LoadUserData()
    {
        PlayerData pData = new PlayerData(new GameObject("default"));

        PlayFabClientAPI.GetUserData(new GetUserDataRequest(),
            (GetUserDataResult result) =>
            {
                if (result.Data != null)
                {
                    if (result.Data.ContainsKey("position"))
                    {
                        pData.position = CSVToArray(result.Data["position"].Value);
                    }

                    if (result.Data.ContainsKey("rotation"))
                    {
                        pData.rotation = CSVToArray(result.Data["rotation"].Value);
                    }

                    cachedData = pData;
                }
            }, OnError);
    }

    // load user data and invoke callback function when done
    public static void LoadUserData(Action<PlayerData> OnDataReceived)
    {
        PlayerData pData = new PlayerData(new GameObject("default"));

        PlayFabClientAPI.GetUserData(new GetUserDataRequest(),
            (GetUserDataResult result) =>
            {
                if (result.Data != null)
                {
                    if (result.Data.ContainsKey("position"))
                    {
                        pData.position = CSVToArray(result.Data["position"].Value);
                    }

                    if (result.Data.ContainsKey("rotation"))
                    {
                        pData.rotation = CSVToArray(result.Data["rotation"].Value);
                    }

                    cachedData = pData;
                    OnDataReceived(pData);
                }
            }, OnError);
    }

    private static string ArrayToCSV(float[] data)
    {
        string output = "";
        for (int i = 0; i < data.Length; i++)
        {
            if (i > 0)
            {
                output = output + ",";
            }
            output = output + data[i];
        }
        return output;
    }

    private static float[] CSVToArray(string data)
    {
        string[] strData = data.Split(',');
        float[] fData = new float[strData.Length];

        for (int i = 0; i < strData.Length; i++)
        {
            fData[i] = float.Parse(strData[i]);
        }

        return fData;
    }

    private static void OnError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }

    private static void OnError(string error)
    {
        Debug.Log(error);
    }
}
