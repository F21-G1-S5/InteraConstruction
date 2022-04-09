using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DumpTruckMovement : MonoBehaviour, InteractiveMachine {
    float speed = 5.0f;
    float angularSpeed = 45.0f;

    [SerializeField] DumpTruckDump dumpTruckDump;
    [SerializeField] float boxRotateSpeed = 0.05f;

    [SerializeField] private Transform operatingPosition;
    [SerializeField] private Transform dismountPosition;

    public KeyCode activator;
    public GameObject target;

    private GameObject operatingPlayer;

    // Update is called once per frame
    /// <summary>
    /// method <c>Update</c> calls lifting and lowering methods of the scissorlift when the player hits the Z and C keys
    /// </summary>
    void Update() {
        
    }

    /// <summary>
    /// When the player presses the interact key, call this function
    /// </summary>
    /// <param name="player">the object trying to interact</param>
    /// <returns>Returns a reference to the machine, or null if the machine is being operated by someone else</returns>
    public InteractiveMachine StartInteraction(GameObject player) {
        if(operatingPlayer) {
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
    public void EndInteraction(GameObject player) {
        operatingPlayer = null;
        player.transform.parent = null;
        player.transform.position = dismountPosition.position;
        player.transform.rotation = dismountPosition.rotation;
    }
    public void Operate() {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        transform.Rotate(0, h * angularSpeed * Time.deltaTime, 0);
        transform.Translate(0, 0, v * speed * Time.deltaTime);

        // rasing and lowering dump truck box
        var r = Input.GetAxisRaw("Fire1");
        var l = Input.GetAxisRaw("Fire2");
        if(r > 0) {
            dumpTruckDump.Lift(boxRotateSpeed * Time.deltaTime);
            dumpTruckDump.PlayLiftAudio();
        }
        else if (l>0) {
            dumpTruckDump.Lower(boxRotateSpeed * Time.deltaTime);
            dumpTruckDump.PlayLiftAudio();
        }
        else {
            dumpTruckDump.StopLiftAudio();
        }
    }
}