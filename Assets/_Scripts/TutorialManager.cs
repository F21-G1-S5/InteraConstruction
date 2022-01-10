using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The tutorial manager manages hiding/showing different ui objects given by PromptTutDes objects,
 * in such a way that only the most recent tutorial is visible (no overalpping ui's)
 * 
 * It's designed this way so as to not change the way Tutorial Descriptions are added to machines
 */

public class TutorialManager : MonoBehaviour
{
    List<GameObject> tutorials;
    int head = -1;

    // Start is called before the first frame update
    void Start()
    {
        tutorials = new List<GameObject>();
    }

    /// <summary>
    /// Activates the given <c>GameObject</c> and adds it to the list of managed objects.
    /// Adding a new object hides the previous one.
    /// </summary>
    /// <param name="ui"></param>
    public void ShowTutorial(GameObject ui)
    {
        // if object is already in the list, remove it (then it will be re-inserted at the top)
        for (int i = 0; i < tutorials.Count; i++)
        {
            if (tutorials[i] == ui)
            {
                tutorials.RemoveAt(i);
                head--;
                break;
            }
        }

        tutorials.Add(ui);
        ui.SetActive(true);
        head++;
        if (head > 0)
        {
            tutorials[head - 1].SetActive(false);
        }
    }

    /// <summary>
    /// Hides the given <c>GameObject</c> and attempts to remove it from the list of managed objects.
    /// If the object if found in the list, the previous object is set active.
    /// </summary>
    /// <param name="ui"></param>
    public void HideTutorial(GameObject ui)
    {
        ui.SetActive(false);

        for (int i = 0; i <= head; i++)
        {
            if (ui == tutorials[i])
            {
                tutorials.RemoveAt(i);
                head--;

                if (head >= 0)
                {
                    tutorials[head].SetActive(true);
                }

                break;
            }
        }
    }
}
