using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// Class <c>ForkLoaderMovement</c> controls a fork loader object by responding to user inputs.
/// Attach this MonoBehaviour as a component to an object to enable these controls.
/// </summary>
public class ForkLoaderMovement : MonoBehaviourPunCallbacks, InteractiveMachine
{
    [SerializeField] private float speed = 6.0f;
    [SerializeField] private float angularSpeed = 45.0f;

    [SerializeField] private Transform operatingPosition;
    [SerializeField] private Transform dismountPosition;
    [SerializeField] ForkLoaderForks forks;
    [SerializeField] float forksSpeed;

    public KeyCode tutorialPanelActivator;
    public GameObject target;

    private GameObject operatingPlayer;

    //Variables for progression system
    [SerializeField] private SOProgressPoint checkpoint1;
    [SerializeField] private SOProgressPoint checkpoint2;
    [SerializeField] private SOProgressPoint checkpoint3;
    [SerializeField] private SOProgressPoint checkpoint4;
    [SerializeField] private GameObject progressionPanelForkloader;

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
        int r = player.GetInstanceID();

        PlayerMovement pc = player.GetComponent<PlayerMovement>();
        if (pc)
        {
            pc.SetPlayerSitting();

            if (pc.isLocalPlayer())
            {
                progressionPanelForkloader.SetActive(true);
            }
        }

        forks.PlayStartUpAudio();
        forks.PlayIdleAudio();

        return this;
    }

    /// <summary>
    /// End the interaction
    /// </summary>
    /// <param name="player">the object trying to leave the machine</param>
    public void EndInteraction(GameObject player)
    {
        forks.StopStartUpAudio();
        forks.StopIdleAudio();
        operatingPlayer = null;
        player.transform.parent = null;
        player.transform.position = dismountPosition.position;
        player.transform.rotation = dismountPosition.rotation;

        progressionPanelForkloader.SetActive(false);
    }

    /// <summary>
    /// Calls the machine's own Update function, allowing it to react to user input and perform other
    /// "operations" while a player is using it.
    /// </summary>
    public void Operate()
    {

        // controls for moving the fork
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        transform.Rotate(0, h * angularSpeed * Time.deltaTime, 0);
        transform.Translate(0, 0, v * speed * Time.deltaTime);

        //Progression Related: check if player rotated the bulldozer
        if (h > 0)
        {
            if (!checkpoint1.IsCompleted)
            {
                checkpoint1.SetCompleted();
            }

        }

        //Progression Related: check if player drove the bulldozer
        if (v > 0)
        {
            if (checkpoint1.IsCompleted && !checkpoint2.IsCompleted)
            {
                checkpoint2.SetCompleted();
            }

        }

        // controls for raising and lowering the bulldozer blade
        var raise = Input.GetAxis("Fire1");
        var lower = Input.GetAxis("Fire2");
        if (raise > 0)
        {
            forks.Lift(forksSpeed * Time.deltaTime);
            forks.PlayLiftAudio();
            
        }
        else if (lower > 0)
        {
            forks.Lower(forksSpeed * Time.deltaTime);
            forks.PlayLiftAudio();

            if (checkpoint2.IsCompleted && !checkpoint3.IsCompleted)
            {
                checkpoint3.SetCompleted();
            }
        }
        else
        {
            forks.StopLiftAudio();
        }

        if (Input.GetKeyDown(KeyCode.G))
        { // Froce grap to grab items from height
            forks.PickUpItem();


            if (checkpoint3.IsCompleted && !checkpoint4.IsCompleted)
            {
                checkpoint4.SetCompleted();
            }
        }

        if (Input.GetKeyDown(KeyCode.B)) { // Force release to put objects up higher
            forks.PutDownItem();
        }


    }
}
