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

        if (tManager == null)
        {
            tManager = FindObjectOfType<TutorialManager>();
        }
    }

    private void OnTriggerEnter(Collider player)
    {
        if (tManager != null)
        {
            if (player.gameObject.tag == "Player")
            {
                PlayerMovement pc = player.gameObject.GetComponent<PlayerMovement>();
                if (pc)
                {
                    // show/hide tutorial if the local player is the one triggering it
                    if (pc.isLocalPlayer())
                    {
                        tManager.ShowTutorial(uiObject);
                    }
                    return;
                }
                // default behaviour if no PlayerMovement found
                tManager.ShowTutorial(uiObject);
            }
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
            if (other.gameObject.tag == "Player")
            {
                PlayerMovement pc = other.gameObject.GetComponent<PlayerMovement>();
                if (pc)
                {
                    // show/hide tutorial if the local player is the one triggering it
                    if (pc.isLocalPlayer())
                    {
                        tManager.HideTutorial(uiObject);
                    }
                    return;
                }
                // default behaviour if no PlayerMovement found
                tManager.HideTutorial(uiObject);
            }
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
