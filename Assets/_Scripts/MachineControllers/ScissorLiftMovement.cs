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

    public void EndInteraction(GameObject player)
    {
        operatingPlayer = null;
        player.transform.parent = null;
        player.transform.position = dismountPosition.position;
        player.transform.rotation = dismountPosition.rotation;
    }

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
