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
            // desyncs, either in the editor or with use save data, were causing the progress system
            // to get stuck when the next progress point is already set to "completed"
            // It's up to the developers to make sure the progress points are linked correctly
            // in the scene, but following line should help clear up inconsistencies we cause.
            progressPoints[currentProgress].IsCompleted = false;
        }
    }

    void Start()
    {
        // read the initial state of each progressToken. The SceneProgressManager may have reset these on start (using save data)
        uiText.text = "All Objectives Completed!";
        for (int i = 0; i < progressPoints.Length; i++)
        {
            if (progressPoints[i].IsCompleted)
            {
                // this point was already completed, so go next
                currentProgress++;
            }
            else
            {
                // this point hasn't been completed by the player, so we can start from here
                uiText.text = progressPoints[currentProgress].Description;
                break;
            }
        }

        // register callback for each progressToken
        foreach (SOProgressPoint point in progressPoints)
        {
            point.Listen(OnProgressCompleted);
        }
    }

    public void ResetProgress()
    {
        // resetting progress has been moved here. The SceneProgressManager is now responsible for
        // setting all progress when the scene loads(using saved data).
        // Use this function to reset only progress tokens managed by this Progress Manager
        foreach (SOProgressPoint point in progressPoints)
        {
            point.IsCompleted = false;
        }
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
