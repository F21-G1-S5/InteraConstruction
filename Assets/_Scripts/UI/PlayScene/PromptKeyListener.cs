using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptKeyListener : MonoBehaviour
{
    [SerializeField] private TutorialPrompt tutorial;
    public bool isTutorialActive = false;

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
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (isTutorialActive)
            {
                tutorial.HideTutorial();
            }
            else
            {
                tutorial.ShowTutorial();
            }
        }
    }

    public void TriggerHideTutorial()
    {
        tutorial.HideTutorial();
    }
}
