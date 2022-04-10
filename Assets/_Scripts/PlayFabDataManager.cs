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
    private static bool triggerReload = false;

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
            (UpdateUserDataResult result) => { Debug.Log("Sucessfully save player data!"); triggerReload = true; },
            OnError);
    }

    public static void SaveUserProgress(PlayerData pData)
    {
        string position = ArrayToCSV(pData.position);
        string rotation = ArrayToCSV(pData.rotation);
        string progress = ArrayToCSV(pData.progress);
        Debug.Log(progress);

        var userTypeRequest = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { "userType", userType },
                { "position", position },
                { "rotation", rotation },
                { "progress", progress }
            }
        };
        PlayFabClientAPI.UpdateUserData(userTypeRequest,
            (UpdateUserDataResult result) => { Debug.Log("Sucessfully save player data!"); triggerReload = true; },
            OnError);
    }

    public static void SaveUserData(GameObject go)
    {
        PlayerData pData = new PlayerData(go);
        SaveUserData(pData);
    }

    public static void SaveUserData(GameObject go, bool[] progressTokens)
    {
        PlayerData pData = new PlayerData(go, progressTokens);
        SaveUserProgress(pData);
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

                    if (result.Data.ContainsKey("progress"))
                    {
                        pData.progress = CSVToBoolArray(result.Data["progress"].Value);
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

                    if (result.Data.ContainsKey("progress"))
                    {
                        pData.progress = CSVToBoolArray(result.Data["progress"].Value);
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

    private static string ArrayToCSV(bool[] data)
    {
        string output = "";
        for (int i = 0; i < data.Length; i++)
        {
            if (i > 0)
            {
                output = output + ",";
            }
            if (data[i])
            {
                output = output + "1";
            }
            else
            {
                output = output + "0";
            }
        }
        return output;
    }

    private static bool[] CSVToBoolArray(string data)
    {
        string[] strData = data.Split(',');
        bool[] fData = new bool[strData.Length];

        for (int i = 0; i < strData.Length; i++)
        {
            // a value of "1" equates to true, anything else will equate to false
            fData[i] = strData[i].Equals("1");
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

    // if we ever want the previously polled user data, without having to query PlayFab unnecessarily,
    // use this function to read the cached data from the previous query
    public static PlayerData GetCachedData()
    {
        if (triggerReload)
        {
            // if we've recently saved data and know that the cached data may not be
            // accurate, query PlayFab for the updated set
            LoadUserData();
        }
        if (cachedData == null)
        {
            // if we have not connected to PlayFab
            return null;
        }
        return new PlayerData(PlayFabDataManager.cachedData);
    }
}
