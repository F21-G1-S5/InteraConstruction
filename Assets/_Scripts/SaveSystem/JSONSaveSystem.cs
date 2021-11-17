using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// Saves and loads serializable PlayerData to a JSON file.
public static class JSONSaveSystem
{
    private static string filePath = "Assets/Resources/Saves/";

    // TODO: when someone creates the player controller, we need to change the parameter type from 'GameObject' to the more
    //       specific player object.
    public static void SavePlayer(GameObject p, string filename)
    {
        // extract serializable player data
        PlayerData pData = new PlayerData(p);

        // create json
        string data = JsonUtility.ToJson(pData);

        // open file stream
        FileStream stream = new FileStream(filePath + filename + ".json", FileMode.Create);
        StreamWriter sw = new StreamWriter(stream);

        stream.SetLength(0); // delete previous contents
        sw.Write(data); // write data

        // save and close stream
        sw.Flush();
        stream.Close();
    }

    public static PlayerData LoadPlayer(string filename)
    {
        if (File.Exists(filePath))
        {
            FileStream stream = new FileStream(filePath + filename + ".json", FileMode.Open);
            StreamReader sr = new StreamReader(stream);

            string data = sr.ReadToEnd();

            stream.Close();

            // convert json into serializable data
            PlayerData pData = JsonUtility.FromJson<PlayerData>(data);

            return pData;
        }
        else
        {
            Debug.Log("Save file not found at " + filePath + filename + "json");
            return null;
        }
    }
}
