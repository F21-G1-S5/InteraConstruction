using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds a reference ALL progress tokens in a scene, and allows a scene to load the status of each progress
/// token given an array of booleans.
/// 
/// For more specific collections of progress tokens with special behavior on completion of these tasks,
/// see ProgressManager
/// </summary>
public class SceneProgressManager : MonoBehaviour
{
    [SerializeField] private SOProgressPoint[] progressTokens;

    // Start is called before the first frame update
    void Awake()
    {
        PlayerData pData = PlayFabDataManager.GetCachedData();

        if (pData != null && pData.progress != null)
        {
            for (int i = 0; i < progressTokens.Length; i++)
            {
                if (i < pData.progress.Length)
                {
                    progressTokens[i].IsCompleted = pData.progress[i];
                }
                else
                {
                    // tokens that aren't in the saved data default to false
                    progressTokens[i].IsCompleted = false;
                }
            }
        }
        else
        {
            // if there was no cached progress data, we can just set all progression to the initial state
            foreach(SOProgressPoint p in progressTokens)
            {
                p.IsCompleted = false;
            }
        }
    }

    public bool[] GetProgressBools()
    {
        bool[] states = new bool[progressTokens.Length];

        for (int i = 0; i < progressTokens.Length; i++)
        {
            states[i] = progressTokens[i].IsCompleted;
        }

        return states;
    }
}
