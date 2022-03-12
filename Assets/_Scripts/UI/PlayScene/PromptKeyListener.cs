using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
        tutorial.HideTutorial();
    }
}
