using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <c>TutorialPrompt</c> handles logic for the buttons on the tutorial prompt and the tutorial screen.
/// This component should be attached to the canvas which contains these two objects.
/// </summary>
public class TutorialPrompt : MonoBehaviour
{
    public GameObject tutorialScreen;
    public GameObject minimap;
    public GameObject pauseButton;
    public GameObject prompt;

    public void ShowTutorial()
    {
        minimap.SetActive(false);
        pauseButton.SetActive(false);
        prompt.SetActive(false);

        tutorialScreen.SetActive(true);

        Pause_Resume.GamePaused = true;
    }

    public void HideTutorial()
    {
        minimap.SetActive(true);
        pauseButton.SetActive(true);

        tutorialScreen.SetActive(false);

        Pause_Resume.GamePaused = false;
    }
}
