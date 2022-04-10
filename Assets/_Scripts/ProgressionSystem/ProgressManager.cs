using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// example progress management object, which maintains a list of progress points or tokens for the scene.
// The porgress manager can also contain funcitonality that generically applies to every progress point in the scene,
// such as displaying a message whenever any progress is completed
public class ProgressManager : MonoBehaviour
{
    [SerializeField] private SOProgressPoint[] progressPoints;

    // callback action whenever some progres is completed.
    // in this case we'll update a UI element in the scene
    private int currentProgress = 0;
    [SerializeField] private Text uiText;
    private void OnProgressCompleted()
    {
        currentProgress++;
        if (currentProgress >= progressPoints.Length)
        {
            uiText.text = "All Objectives Completed!";


        }
        else
        {
            uiText.text = progressPoints[currentProgress].Description;
        }
    }

    void Start()
    {
        // reset all progression when the scene starts (in the actual sim, we might want to check the player's save data here)
        foreach (SOProgressPoint point in progressPoints)
        {
            point.IsCompleted = false;
            // also listen for updates using the callback action
            point.Listen(OnProgressCompleted);
        }

        // display the first objective
        uiText.text = progressPoints[0].Description;
    }

    // The progress manager can also have other useful functions. For our project, this may
    // include converting the progress list into a simple array that we can save to our user's
    // save data
    bool[] GetProgressArray()
    {
        bool[] userProgress = new bool[progressPoints.Length];
        for (int i = 0; i < progressPoints.Length; i++)
        {
            userProgress[i] = progressPoints[i].IsCompleted;
        }
        return userProgress;
    }
}
