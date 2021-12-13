using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// Class <c>JSONSaveSystem</c> handles saving and loading a gameObject's data to a loval JSON file in a specified folder
/// </summary>
public static class JSONSaveSystem
{
    private static string filePath = Application.persistentDataPath + "/";

    /// <summary>
    /// method <c>SavePlayer</c> saves GameObject p data in the form of a PlayerData object to a local JSON file
    /// </summary>
    /// <param name="p"> the GameObject to be saved</param>
    /// <param name="filename"> the name of the file without file extensions</param>
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

    /// <summary>
    /// method <c>LoadPlayer</c> attemps to create a PlayerData object from file stream located in the specified file
    /// </summary>
    /// <param name="filename">the name of the JSON file to read from, without a file extension</param>
    /// <returns>Returns a PlayerData object from the saved file, or null if the file could not be opened.</returns>
    public static PlayerData LoadPlayer(string filename)
    {
        if (File.Exists(filePath + filename + ".json"))
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
