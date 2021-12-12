using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>ScissozLiftLifting</c> handles movement of the scissorlift, and triggers audio clips for each animation.
/// Use ScissorLiftMovement for updated controls over the animation controller.
/// </summary>
public class ScissorLiftLifting : MonoBehaviour{
    private float liftSpeed = 0.01f;
    private Transform platform;
    private Animator animator;

    public AudioSource scissorliftAudio;
    public AudioSource LiftAudio;

    private void Start() {
        platform=transform.Find("top").transform;
        animator=GetComponent<Animator>();
    }

    private void LiftUp() {
        //ScissorLift.Lift();
        LiftAudio.Play();
        platform.localPosition=new Vector3(platform.localPosition.x,platform.localPosition.y+liftSpeed,platform.localPosition.z);
    }

    private void LiftDown() {
        //ScissorLift.Lower();
        LiftAudio.Play();
        platform.localPosition=new Vector3(platform.localPosition.x,platform.localPosition.y+liftSpeed,platform.localPosition.z);
    }
}
