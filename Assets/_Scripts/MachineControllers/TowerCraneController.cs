using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>TowerCraneController</c> responds to key inputs by moving the crane.
/// Attach this MonoBehaviour as a component to an Object with a <c>TowerCrane</c> component to enable these controls.
/// </summary>
public class TowerCraneController : MonoBehaviour
{
    [SerializeField] private TowerCrane crane;
    [SerializeField] private TowerCraneHookPickup craneHook;
    [SerializeField] private float rotateSpeed = 1f;
    [SerializeField] private float truckSpeed = 1f;
    [SerializeField] private float hookSpeed = 1f;

    // Update is called once per frame
    void Update()
    {
        // horizontal movement of the hook truck
        if (Input.GetKey(KeyCode.W))
        {
            crane.MoveTruck(-truckSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            crane.MoveTruck(truckSpeed * Time.deltaTime);
        }

        // vertical movement of the hook
        if (Input.GetKey(KeyCode.Z))
        {
            crane.MoveHook(-hookSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.C))
        {
            crane.MoveHook(hookSpeed * Time.deltaTime);
        }

        // rotation of the crane
        if (Input.GetKey(KeyCode.A))
        {
            crane.Rotate(-rotateSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            crane.Rotate(rotateSpeed * Time.deltaTime);
        }

        // item pick up and drop
        if (Input.GetKeyDown(KeyCode.G)) {
            craneHook.PickUpItem();
        }
        if (Input.GetKeyDown(KeyCode.B)) {
            craneHook.PutDownItem();
        }
    }
}
