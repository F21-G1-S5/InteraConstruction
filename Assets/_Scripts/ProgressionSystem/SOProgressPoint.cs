using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ProgressPoint", menuName = "ScriptableObjects/ProgressPointSO", order = 1)]
public class SOProgressPoint : ScriptableObject
{
    // an object representing a task to be completed by the player
    // there is no actual game logic here, just the status of the task and
    // possibly a description
    public bool IsCompleted = false;
    public string Description = "";

    // Passively keeping track of progress is cool and all, but we may want to do something
    // when something gets completed. In this case, we'll save a callback function and notify
    // the progress manager that progress was updated.
    private UnityEvent OnComplete;

    public void Listen(UnityAction onCompleted)
    {
        OnComplete = new UnityEvent();
        OnComplete.AddListener(onCompleted);
    }

    public void SetCompleted()
    {
        IsCompleted = true;
        OnComplete.Invoke();
    }
}
