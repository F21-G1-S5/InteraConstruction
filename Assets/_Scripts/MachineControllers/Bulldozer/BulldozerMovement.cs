using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// Class <c>BulldozerMovement</c> controls a bulldozer object by responding to user inputs.
/// Attach this MonoBehaviour as a component to an object to enable these controls.
/// </summary>
public class BulldozerMovement : MonoBehaviourPunCallbacks, InteractiveMachine
{
    float speed = 5.0f;
    float angularSpeed = 45.0f;
    bool progressCounter=false;

    [SerializeField] DozerBlade dozerBlade; // the blade part of the bulldozer that can move up and down
    [SerializeField] float bladeSpeed = 0.4f;

    [SerializeField] private Transform operatingPosition;
    [SerializeField] private Transform dismountPosition;

    public KeyCode activator;
    public GameObject target;

    public GameObject BDPP1Text;
    public GameObject BDPP2Text;
    public GameObject BDPP3Text;
    public GameObject BDPP4Text;
    public GameObject BDPPsCompletedText;

    private GameObject operatingPlayer;

    //Variables for progression system
    [SerializeField] private SOProgressPoint checkpoint1;
    [SerializeField] private SOProgressPoint checkpoint2;
    [SerializeField] private SOProgressPoint checkpoint3;
    [SerializeField] private SOProgressPoint checkpoint4;

    // Start is called before the first frame update
    void Start()
    {

        if (dozerBlade == null)
        {
            dozerBlade = GetComponent<DozerBlade>();
        }
    }

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
        int r = player.GetInstanceID();

        PlayerMovement pc = player.GetComponent<PlayerMovement>();
        if (pc)
        {
            pc.SetPlayerSitting();
        }

        dozerBlade.PlayStartUpAudio();
        dozerBlade.PlayIdleAudio();
        BDPP1Text.SetActive(true);
        BDPP2Text.SetActive(true);
        BDPP3Text.SetActive(true);
        BDPP4Text.SetActive(true);

        return this;
    }

    /// <summary>
    /// End the interaction
    /// </summary>
    /// <param name="player">the object trying to leave the machine</param>
    public void EndInteraction(GameObject player)
    {
        dozerBlade.StopStartUpAudio();
        dozerBlade.StopIdleAudio();
        operatingPlayer = null;
        player.transform.parent = null;
        player.transform.position = dismountPosition.position;
        player.transform.rotation = dismountPosition.rotation;
        BDPPsCompletedText.SetActive(false);
        if (checkpoint1.IsCompleted && checkpoint2.IsCompleted && checkpoint3.IsCompleted && checkpoint4.IsCompleted)
        {
            progressCounter = true; // Logic to know that the player exited the Bulldozer with all checkpoints done so in case
                                    //the same player wants to enter the Bulldozer again, the checkpoints don´t reappear.
        }
    }

    /// <summary>
    /// Calls the machine's own Update function, allowing it to react to user input and perform other
    /// "operations" while a player is using it.
    /// </summary>
    public void Operate()
    {
        if (checkpoint1.IsCompleted && checkpoint2.IsCompleted && checkpoint3.IsCompleted && checkpoint4.IsCompleted)
        {
            BDPPsCompletedText.SetActive(true);

            if(progressCounter)
            {
                BDPP1Text.SetActive(false);
                BDPP2Text.SetActive(false);
                BDPP3Text.SetActive(false);
                BDPP4Text.SetActive(false);
                BDPPsCompletedText.SetActive(false);
            }
        }

        // controls for moving the bulldozer
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");

        transform.Rotate(0, h * angularSpeed * Time.deltaTime, 0);
        transform.Translate(0, 0, v * speed * Time.deltaTime);

        //Progression Related: check if player rotated the bulldozer
        if(h > 0)
        {
            if (!checkpoint1.IsCompleted)
            {
                checkpoint1.SetCompleted();
            }
            BDPP2Text.SetActive(false);
        }

        //Progression Related: check if player drove the bulldozer
        if (v > 0)
        {
            if (!checkpoint2.IsCompleted)
            {
                checkpoint2.SetCompleted();
            }
            BDPP1Text.SetActive(false);
        }

        // controls for raising and lowering the bulldozer blade
        var raise = Input.GetAxis("Fire1");
        var lower = Input.GetAxis("Fire2");
        if (raise > 0)
        {
            BDPP3Text.SetActive(false);
            dozerBlade.Lift(bladeSpeed * Time.deltaTime);
            dozerBlade.PlayLiftAudio();
            if (!checkpoint3.IsCompleted)
            {
                checkpoint3.SetCompleted();
            }
        }
        else if (lower > 0)
        {
            BDPP4Text.SetActive(false);
            dozerBlade.Lower(bladeSpeed * Time.deltaTime);
            dozerBlade.PlayLiftAudio();
            if (!checkpoint4.IsCompleted)
            {
                checkpoint4.SetCompleted();
            }
        }
        else
        {
            dozerBlade.StopLiftAudio();
        }

    }

}
