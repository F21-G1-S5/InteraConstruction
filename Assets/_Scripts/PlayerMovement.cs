using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour{
    private float speed = 0.05f;
    private float gravityMultiplier = 0.007f;
    private float yVel = 0f;
    private CharacterController cc;
    private readonly float gravity = Physics.gravity.y;
    public Transform scissorLift;
    public Transform bulldozer;
    public GameObject targetForTutorial;

    InteractiveMachine operatingMachine;

    //Pase Menu Variables
    //public GameObject targetMenu;
    //public bool isPauseMenuActive;

    private void Start() {
        cc = GetComponent<CharacterController>();
    }
    private void Update() {

        if (operatingMachine != null)
        {
            //operatingMachine.ActivateTutorial()
            if (Input.GetKeyDown(KeyCode.E))
            {
                // return to normal player controls
                operatingMachine.EndInteraction(gameObject);
                operatingMachine = null;
                cc.enabled = true;
            }
            else
            {
                operatingMachine.Operate();
            }
        }
        else
        {
            MovePlayer();
            if (Input.GetKeyDown(KeyCode.E))
            {
                InteractWithMachine();
            }
        }
    }
    private void MovePlayer() {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal")*speed * Time.deltaTime, yVel, Input.GetAxisRaw("Vertical")*speed * Time.deltaTime).normalized;
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.z;
        if (cc.isGrounded && gravity < 0)
            yVel = 0f;
            yVel += -(gravityMultiplier);
        move.y = yVel;
        cc.Move(move);
    }

    /// <summary>
    /// Looks for Interactive Machines, and calls StartInteraction on the first one found.
    /// Sets the operatingMachine reference if one is returned
    /// </summary>
    private void InteractWithMachine()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1f);
        foreach (var collider in hitColliders)
        {
            if (collider.CompareTag("InteractiveMachine"))
            {
                InteractiveMachine machine = collider.gameObject.GetComponent<InteractiveMachine>();
                if (machine != null)
                {
                    operatingMachine = machine.StartInteraction(gameObject);
                    if (operatingMachine != null)
                    {
                        cc.enabled = false;
                    }
                }
                return;
            }
        }
    }


}
