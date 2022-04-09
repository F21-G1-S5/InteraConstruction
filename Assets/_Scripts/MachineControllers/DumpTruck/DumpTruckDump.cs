using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumpTruckDump : MonoBehaviour {
    [SerializeField] Transform dumpBox; // dump truck game object
    [SerializeField] float maxLift = 0.7f; // max angle rads
    [SerializeField] float lift = 0.0f; // initial angle in radss

    //public AudioSource bulldozerAudio;
    public AudioSource LiftAudio;
    public AudioSource startUpAudio;
    public AudioSource idleAudio;

    private float initAngle;

    // Start is called before the first frame update
    void Start() {
        initAngle = dumpBox.localRotation.x;
        Lift(lift);
    }

    /// <summary>
    /// method <c>Lift</c> raises the dump box by the given amount.
    /// Box angle will not exceed <c>maxLift</c> specified in the component.
    /// </summary>
    /// <param name="amount"></param>
    public void Lift(float amount) {
        lift += amount;
        if(lift > maxLift)
            lift = maxLift;
        Quaternion boxAngle = dumpBox.localRotation;
        boxAngle.x = initAngle - lift;
        dumpBox.localRotation = boxAngle;
    }

    /// <summary>
    /// method <c>Lower</c> lowers the dump box by the given amount.
    /// Box angle will not go below 0.
    /// </summary>
    /// <param name="amount"></param>
    public void Lower(float amount) {
        lift -= amount;
        if(lift < 0)
            lift = 0;
        Quaternion boxAngle = dumpBox.localRotation;
        boxAngle.x = initAngle - lift;
        dumpBox.localRotation = boxAngle;
    }

    public void PlayLiftAudio() {
        if(!LiftAudio.isPlaying) {
            LiftAudio.Play();
        }
    }

    public void StopLiftAudio() {
        LiftAudio.Stop();
    }

    public void PlayStartUpAudio() {
        startUpAudio.Play();
    }

    public void StopStartUpAudio() {
        startUpAudio.Stop();
    }

    public void PlayIdleAudio() {
        idleAudio.PlayDelayed(startUpAudio.clip.length); //play audio after startup clip has ended
    }

    public void StopIdleAudio() {
        idleAudio.Stop();
    }
}
