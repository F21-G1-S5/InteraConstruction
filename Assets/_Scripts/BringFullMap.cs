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

    // Update is called once per frame
    void Update()
    {
        ActivateFullMap();
    }

    private void ActivateFullMap()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (fullMapOpen)
            {
                TargetMap.SetActive(false);
                MiniMap.SetActive(true);
                Cursor.visible = false;
                fullMapOpen = false;
                Debug.Log("Full map inactive");
            }
            else
            {
                TargetMap.SetActive(true);
                MiniMap.SetActive(false);
                Cursor.visible = true;
                fullMapOpen = true;
                Debug.Log("Full map active");
            }
        }

    }
}
