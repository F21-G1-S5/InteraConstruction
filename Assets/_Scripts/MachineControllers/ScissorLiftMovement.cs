using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>ScissorLiftMovement</c> receives inputs from the player to control the ScissorLift prefab
/// </summary>
public class ScissorLiftMovement : MonoBehaviour, InteractiveMachine
{
    [SerializeField] ScissorLift lift;

    [SerializeField] private Transform operatingPosition;
    [SerializeField] private Transform dismountPosition;

    public KeyCode activator;
    public GameObject target;

    private GameObject operatingPlayer;

    // Update is called once per frame
    /// <summary>
    /// method <c>Update</c> calls lifting and lowering methods of the scissorlift when the player hits the Z and C keys
    /// </summary>
    void Update()
    {
        if (CameraSwitching.inScissorLift)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                lift.Lift();
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                lift.Lower();
            }
        }
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
        if (Input.GetKeyDown(KeyCode.C))
        {
            lift.Lift();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            lift.Lower();
        }
    }
}
