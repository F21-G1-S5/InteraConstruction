using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>DozerBlade</c> handles the specific movement of the bulldozerblade.
/// Attach this MonoBehaviour as a component, along with a <c>BulldozerMovement</c> component, to enable these controls
/// </summary>
public class DozerBlade : MonoBehaviour
{
    [SerializeField] Transform bladePivot;
    [SerializeField] float maxLift = 1; // max angle rads
    [SerializeField] float lift = 0; // initial angle in radss

    //public AudioSource bulldozerAudio;
    public AudioSource LiftAudio;
    public AudioSource startUpAudio;
    public AudioSource idleAudio;

    private float rootAngle;

    private void Start()
    {
        // set the root rotation for the object. Additional transformations are relative to this
        rootAngle = bladePivot.localRotation.x;
        Lift(lift);
    }

    /// <summary>
    /// method <c>Lift</c> raises the bulldozer blade by the given amount.
    /// Blade angle will not exceed <c>maxLift</c> specified in the component.
    /// </summary>
    /// <param name="amount"></param>
    public void Lift(float amount)
    {
        lift += amount;
        if (lift > maxLift) lift = maxLift;
        Quaternion bladeAngle = bladePivot.localRotation;
        bladeAngle.x = rootAngle - lift;
        bladePivot.localRotation = bladeAngle;
    }

    /// <summary>
    /// method <c>Lower</c> lowers the bulldozer blade by the given amount.
    /// Blade angle will not go below 0.
    /// </summary>
    /// <param name="amount"></param>
    public void Lower(float amount)
    {
        lift -= amount;
        if (lift < 0) lift = 0;
        Quaternion bladeAngle = bladePivot.localRotation;
        bladeAngle.x = rootAngle - lift;
        bladePivot.localRotation = bladeAngle;
    }

    public void PlayLiftAudio()
    {
        if (!LiftAudio.isPlaying)
        {
            LiftAudio.Play();
        }
    }

    public void StopLiftAudio()
    {
        LiftAudio.Stop();
    }

    public void PlayStartUpAudio()
    {
        startUpAudio.Play();
    }

    public void StopStartUpAudio()
    {
        startUpAudio.Stop();
    }

    public void PlayIdleAudio()
    {
        idleAudio.PlayDelayed(startUpAudio.clip.length); //play audio after startup clip has ended
    }

    public void StopIdleAudio()
    {
        idleAudio.Stop();
    }
}
