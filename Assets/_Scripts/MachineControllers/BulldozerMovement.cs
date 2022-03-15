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
        if (CameraSwitching.inBulldozer)
        {
            // Making the current player the owner 
            //photonView.RequestOwnership();

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
