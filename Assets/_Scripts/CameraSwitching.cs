using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitching : MonoBehaviour
{
    public GameObject camCrane;
    public TowerCraneController craneController; // reference to controller, so we can turn it on or off
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
            craneController.enabled = true;

            camBulldozer.SetActive(false);
            camScissorLift.SetActive(false);
            MainCamera.SetActive(false);
            
        }
        if (Input.GetButtonDown("Bulldozer"))
        {
            playerInVehicle = true;
            inBulldozer = true;
            camCrane.SetActive(false);
            craneController.enabled = false;

            camBulldozer.SetActive(true);
            camScissorLift.SetActive(false);
            MainCamera.SetActive(false);
        }
        if (Input.GetButtonDown("ScissorLift"))
        {
            playerInVehicle = true;
            inScissorLift = true;
            camCrane.SetActive(false);
            craneController.enabled = false;

            camBulldozer.SetActive(false);
            camScissorLift.SetActive(true);
            MainCamera.SetActive(false);
        }
        if (Input.GetButtonDown("MainCamera"))
        {
            camCrane.SetActive(false);
            craneController.enabled = false;

            camBulldozer.SetActive(false);
            camScissorLift.SetActive(false);
            MainCamera.SetActive(true);
        }

    }
}
