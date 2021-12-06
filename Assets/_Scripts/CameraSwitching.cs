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

    public static bool playerInVehicle = false;
    public static bool inScissorLift = false;
    public static bool inBulldozer = false;

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
            playerInVehicle = true;
            inBulldozer = true;
            camCrane.SetActive(false);
            camBulldozer.SetActive(true);
            camScissorLift.SetActive(false);
            MainCamera.SetActive(false);
        }
        if (Input.GetButtonDown("ScissorLift"))
        {
            playerInVehicle = true;
            inScissorLift = true;
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
