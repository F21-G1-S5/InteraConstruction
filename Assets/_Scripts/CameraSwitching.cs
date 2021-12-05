using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitching : MonoBehaviour
{
    public GameObject camCrane;
    public GameObject camBulldozer;
    public GameObject camScissorLift;
    public GameObject MainCamera;
    //public GameObject miniMapCamera;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Crane"))
        {
            Debug.Log("pressing Crane Camera");
            camCrane.SetActive(true);
            camBulldozer.SetActive(false);
            camScissorLift.SetActive(false);
            MainCamera.SetActive(false);
            
        }
        if (Input.GetButtonDown("Bulldozer"))
        {
            camCrane.SetActive(false);
            camBulldozer.SetActive(true);
            camScissorLift.SetActive(false);
            MainCamera.SetActive(false);
        }
        if (Input.GetButtonDown("ScissorLift"))
        {
            camCrane.SetActive(false);
            camBulldozer.SetActive(false);
            camScissorLift.SetActive(true);
            MainCamera.SetActive(false);
        }
        if (Input.GetButtonDown("MainCamera"))
        {
            camCrane.SetActive(false);
            camBulldozer.SetActive(false);
            camScissorLift.SetActive(false);
            MainCamera.SetActive(true);
        }

    }
}
