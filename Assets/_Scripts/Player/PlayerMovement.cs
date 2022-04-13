using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// Handles player input and movement.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour{
    private float speed = 0.03f;
    private float gravityMultiplier = 0.007f;
    private float yVel = 0f;
    private CharacterController cc;
    private readonly float gravity = Physics.gravity.y;
    public PeteAnimationController anim;
    
    [SerializeField] private bool singlePlayerMode = false;
    [SerializeField] private PhotonView photonView;
    [SerializeField] private GameObject playerCam;

    public Vector3 moveInput;
    public Vector3 move;

    InteractiveMachine operatingMachine;

    //Pase Menu Variables
    //public GameObject targetMenu;
    //public bool isPauseMenuActive;

    private void Start() {
        cc = GetComponent<CharacterController>();

        if (!singlePlayerMode)
        {
            // remove camera if not the local player object
            if (!photonView.IsMine)
            {
                playerCam.SetActive(false);

            }
            else
            {
                // if we are the local player, link this player with the pause menu for save/load functions
                Pause_Resume pr = FindObjectOfType<Pause_Resume>();
                if (pr)
                {
                    pr.player = this.gameObject;
                }
                // also link with the minimap
                Minimap mm = FindObjectOfType<Minimap>();
                if (mm)
                {
                    mm.player = this.gameObject.transform;
                }
            }
        }
    }
    private void Update()
    {
        if (isLocalPlayer())
        {
            if (operatingMachine != null)
            {
                //operatingMachine.ActivateTutorial()
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // return to normal player controls
                    operatingMachine.EndInteraction(gameObject);
                    
                    // progression panel controls have been moved to the machine controllers
                    // since those machines can manage their own UI without having to search/identify
                    // like the player object had to

                    operatingMachine = null;
                    cc.enabled = true;

                    anim.Idle();
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
    }
    private void MovePlayer() {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal")*speed * Time.deltaTime, yVel, Input.GetAxisRaw("Vertical")*speed * Time.deltaTime).normalized;
        move = transform.right * moveInput.x + transform.forward * moveInput.z;
        if (cc.isGrounded && gravity < 0)
            yVel = 0f;
            yVel += -(gravityMultiplier);
        move.y = yVel;
        cc.Move(move);

        if (move.z != 0)
        {
            anim.Walking();
        }
        else
        {
            anim.Idle();
        }
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

                    if(collider.gameObject.GetComponent<PhotonView>() != null)
                    {

                        collider.gameObject.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
                        Debug.Log(collider.gameObject.GetComponent<PhotonView>().sceneViewId);
                    }

                    operatingMachine = machine.StartInteraction(this.gameObject);

                    if (operatingMachine != null)
                    {
                        cc.enabled = false;
                    }
                }
                return;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>True if the Player object belongs to the local user</returns>
    public bool isLocalPlayer()
    {
        return (singlePlayerMode || photonView.IsMine);
    }

    public void SetPlayerSitting()
    {
        anim.Sitting();
    }
}
