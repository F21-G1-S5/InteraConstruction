using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringFullMap : MonoBehaviour
{
    public GameObject player;

    public GameObject TargetMap;
    public GameObject MiniMap;
    public bool isfullMapOpem = true;
    public static bool fullMapOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        GameObject go = GameObject.Find("TriggerLoadOnStart");

        if (go != null)
        {
            Destroy(go);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ActivateFullMap();
    }

    //Handle pause and resume for ESC key
    private void ActivateFullMap()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (fullMapOpen)
            {

                TargetMap.SetActive(false);
                MiniMap.SetActive(true);
                Time.timeScale = 0.0f;
                Cursor.visible = false;
                fullMapOpen = false;
                Debug.Log("Full map inactive");
            }
            else
            {
                TargetMap.SetActive(true);
                MiniMap.SetActive(false);
                Cursor.visible = true;
                if (isfullMapOpem) Time.timeScale = 0.0f;
                fullMapOpen = true;
                Debug.Log("Full map active");
            }
        }

    }
}
