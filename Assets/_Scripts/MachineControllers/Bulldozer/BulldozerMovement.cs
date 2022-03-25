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
    [SerializeField] DozerBlade dozerBlade; // the blade part of the bulldozer that can move up and down
    [SerializeField] float bladeSpeed = 0.4f;

    [SerializeField] private Transform operatingPosition;
    [SerializeField] private Transform dismountPosition;

    public KeyCode activator;
    public GameObject target;

    private GameObject operatingPlayer;

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
    }

    /// <summary>
    /// Calls the machine's own Update function, allowing it to react to user input and perform other
    /// "operations" while a player is using it.
    /// </summary>
    public void Operate()
    {
        // controls for moving the bulldozer
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        transform.Rotate(0, h * angularSpeed * Time.deltaTime, 0);
        transform.Translate(0, 0, v * speed * Time.deltaTime);

        // controls for raising and lowering the bulldozer blade
        var raise = Input.GetAxis("Fire1");
        var lower = Input.GetAxis("Fire2");
        if (raise > 0)
        {
            dozerBlade.Lift(bladeSpeed * Time.deltaTime);
            dozerBlade.PlayLiftAudio();
        }
        else if (lower > 0)
        {
            dozerBlade.Lower(bladeSpeed * Time.deltaTime);
            dozerBlade.PlayLiftAudio();
        }
        else
        {
            dozerBlade.StopLiftAudio();
        }
    }

}
