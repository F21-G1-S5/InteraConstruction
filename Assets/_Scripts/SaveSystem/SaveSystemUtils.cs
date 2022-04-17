using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides a set of helper functions for the save system
/// </summary>
public static class SaveSystemUtils
{
    public static string ArrayToCSV(float[] data)
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

    public static string ArrayToCSV(bool[] data)
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

    public static float[] CSVToArray(string data)
    {
        if (data == "")
        {
            return new float[0];
        }
        string[] strData = data.Split(',');
        float[] fData = new float[strData.Length];

        for (int i = 0; i < strData.Length; i++)
        {
            fData[i] = float.Parse(strData[i]);
        }

        return fData;
    }

    public static bool[] CSVToBoolArray(string data)
    {
        if (data == "")
        {
            return new bool[0];
        }

        string[] strData = data.Split(',');
        bool[] fData = new bool[strData.Length];

        for (int i = 0; i < strData.Length; i++)
        {
            // a value of "1" equates to true, anything else will equate to false
            fData[i] = strData[i].Equals("1");
        }

        return fData;
    }
}
