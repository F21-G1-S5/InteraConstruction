using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>ScissorLiftMovement</c> receives inputs from the player to control the ScissorLift prefab
/// </summary>
public class ScissorLiftMovement : MonoBehaviour
{
    float speed = 5.0f;
    float angularSpeed = 45.0f;
    [SerializeField] ScissorLift lift;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    /// <summary>
    /// method <c>Update</c> calls lifting and lowering methods of the scissorlift when the player hits the Z and C keys
    /// </summary>
    void Update()
    {
        if (CameraSwitching.inScissorLift)
        {
            if (Input.GetKey(KeyCode.C))
            {
                lift.Lift();
            }
            if (Input.GetKey(KeyCode.Z))
            {
                lift.Lower();
            }
        }
    }
}
