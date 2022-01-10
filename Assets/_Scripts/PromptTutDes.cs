using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptTutDes : MonoBehaviour
{
    public GameObject uiObject; // this gameobject should be the tutorial prompt that shows up at the bottom of the screen
    public TutorialManager tManager;

    // Start is called before the first frame update
    void Start()
    {
        uiObject.SetActive(false);

        tManager = FindObjectOfType<TutorialManager>();
    }

    private void OnTriggerEnter(Collider player)
    {
        if (tManager != null)
        {
            tManager.ShowTutorial(uiObject);
        }
        else
        {
            // support for old behavior without a tutorial manager
            if (player.gameObject.tag == "Player")
            {
                uiObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (tManager != null)
        {
            tManager.HideTutorial(uiObject);
        }
        else
        {
            if (other.gameObject.tag == "Player")
            {
                uiObject.SetActive(false);
            }
        }
    }
}
