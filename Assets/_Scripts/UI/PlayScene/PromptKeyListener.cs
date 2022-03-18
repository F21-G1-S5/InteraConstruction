using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Provides a generic way for UI elements to trigger events when a key is pressed.
/// KeyCide abd UnityEvent should be set in the editor.
/// </summary>
public class PromptKeyListener : MonoBehaviour
{
    [SerializeField] private TutorialPrompt tutorial;

    [SerializeField] private KeyCode key;
    [SerializeField] private UnityEvent onKeyPressed;

    private void Start()
    {
        if (tutorial == null)
        {
            tutorial = GetComponentInParent<TutorialPrompt>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            onKeyPressed.Invoke();
        }
    }

    public void TriggerHideTutorial()
    {
        tutorial?.HideTutorial();
    }
}
