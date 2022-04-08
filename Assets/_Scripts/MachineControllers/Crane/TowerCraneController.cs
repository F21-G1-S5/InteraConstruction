using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>TowerCraneController</c> responds to key inputs by moving the crane.
/// Attach this MonoBehaviour as a component to an Object with a <c>TowerCrane</c> component to enable these controls.
/// </summary>
public class TowerCraneController : MonoBehaviour, InteractiveMachine
{
    [SerializeField] private TowerCrane crane;
    [SerializeField] private TowerCraneHookPickup craneHook;
    [SerializeField] private float rotateSpeed = 1f;
    [SerializeField] private float truckSpeed = 1f;
    [SerializeField] private float hookSpeed = 1f;

    [SerializeField] private Transform operatingPosition;
    [SerializeField] private Transform dismountPosition;

    public KeyCode tutorialPanelActivator;
    public GameObject target;

    private GameObject operatingPlayer;

    //Variables for progression system
    [SerializeField] private SOProgressPoint checkpoint1;
    [SerializeField] private SOProgressPoint checkpoint2;
    [SerializeField] private SOProgressPoint checkpoint3;
    [SerializeField] private SOProgressPoint checkpoint4;

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// When the player presses the interact key, call this function
    /// </summary>
    /// <param name="player">the object trying to interact</param>
    /// <returns>Returns a reference to the machine, or null if the machine is being operated by someone else</returns>
    public InteractiveMachine StartInteraction(GameObject player)
    {
        if (operatingPlayer)
        {
            return null;
        }
        operatingPlayer = player;
        player.transform.position = operatingPosition.position;
        player.transform.rotation = operatingPosition.rotation;
        player.transform.parent = operatingPosition;

        PlayerMovement pc = player.GetComponent<PlayerMovement>();
        if (pc)
        {
            pc.SetPlayerSitting();
        }

        return this;
    }

    /// <summary>
    /// End the interaction
    /// </summary>
    /// <param name="player">the object trying to leave the machine</param>
    public void EndInteraction(GameObject player)
    {
        operatingPlayer = null;
        player.transform.parent = null;
        player.transform.position = dismountPosition.position;
        player.transform.rotation = dismountPosition.rotation;
    }

    /// <summary>
    /// Calls the machine's own Update function, allowing it to react to user input and perform other
    /// "operations" while a player is using it.
    /// </summary>
    public void Operate()
    {
        // z-axis movement of the hook
        if (Input.GetKey(KeyCode.W))
        {
            crane.MoveTruck(-truckSpeed * Time.deltaTime);
            if (!checkpoint2.IsCompleted)
            {
                checkpoint2.SetCompleted();
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            crane.MoveTruck(truckSpeed * Time.deltaTime);
            
        }

        // y-axis movement of the hook
        if (Input.GetKey(KeyCode.Z))
        {
            crane.MoveHook(-hookSpeed * Time.deltaTime);
            if (!checkpoint3.IsCompleted)
            {
                checkpoint3.SetCompleted();
            }
        }
        if (Input.GetKey(KeyCode.C))
        {
            crane.MoveHook(hookSpeed * Time.deltaTime);
        }

        // rotation of the crane
        if (Input.GetKey(KeyCode.A))
        {
            crane.Rotate(-rotateSpeed * Time.deltaTime);
            if (!checkpoint1.IsCompleted)
            {
                checkpoint1.SetCompleted();
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            crane.Rotate(rotateSpeed * Time.deltaTime);
        }

        // item pick up and drop
        if (Input.GetKeyDown(KeyCode.G))
        {
            craneHook.PickUpItem();
            if (!checkpoint4.IsCompleted)
            {
                checkpoint4.SetCompleted();
            }
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            craneHook.PutDownItem();
        }
    }
}
