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

    //Variables for progression
    [SerializeField] private GameObject progressionPanelCrane;
    [SerializeField] private GameObject progressionPanelBulldozer;
    [SerializeField] private GameObject progressionPanelForkloader;

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
                    
                    //deactivate progression panels when exiting machines
                    progressionPanelCrane.SetActive(false);
                    progressionPanelBulldozer.SetActive(false);
                    progressionPanelForkloader.SetActive(false);

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
                    //Identify each machine, to activate its correspondent UI Progression panel
                    Debug.Log(machine);
                    switch (machine.ToString())
                    {
                        case "towerCrane_1 (TowerCraneController)":
                            progressionPanelCrane.SetActive(true);
                            break;
                        case "Buldozer_blade (BulldozerMovement)":
                            progressionPanelBulldozer.SetActive(true);
                            break;
                        case "Fork Loader (ForkLoaderMovement)":
                            progressionPanelForkloader.SetActive(true);
                            break;

                    }
                    

                    operatingMachine = machine.StartInteraction(gameObject);
                    if(collider.gameObject.GetComponent<PhotonView>() != null)
                    {

                        collider.gameObject.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
                        Debug.Log(collider.gameObject.GetComponent<PhotonView>().sceneViewId);
                    }
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
