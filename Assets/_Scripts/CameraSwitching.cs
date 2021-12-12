using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>CameraSwitching</c> toggles cameras (and relevant components) on/off fepending on which perspective the player is using
/// </summary>
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
    public static bool inCrane = false;

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

            playerInVehicle = true;
            inScissorLift = false;
            inBulldozer = false;
            inCrane = true;
        }
        if (Input.GetButtonDown("Bulldozer"))
        {
            playerInVehicle = true;
            inScissorLift = false;
            inBulldozer = true;
            inCrane = false;

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
            inBulldozer = false;
            inCrane = false;

            camCrane.SetActive(false);
            craneController.enabled = false;

            camBulldozer.SetActive(false);
            camScissorLift.SetActive(true);
            MainCamera.SetActive(false);
        }
        if (Input.GetButtonDown("MainCamera"))
        {
            playerInVehicle = false;
            inScissorLift = false;
            inBulldozer = false;
            inCrane = false;

            camCrane.SetActive(false);
            craneController.enabled = false;

            camBulldozer.SetActive(false);
            camScissorLift.SetActive(false);
            MainCamera.SetActive(true);
        }

    }
}
