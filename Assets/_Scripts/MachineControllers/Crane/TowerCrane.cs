using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>TowerCrane</c> controls components of a tower crane model and provides method for easily maneuvering
/// these components.
/// </summary>
public class TowerCrane : MonoBehaviour
{
    [SerializeField] Transform cabin;
    [SerializeField] Transform carriage;
    [SerializeField] Transform hook;
    [SerializeField] Transform carriageInner;
    [SerializeField] Transform carriageOuter;
    [SerializeField] Transform hookTop;
    [SerializeField] Transform hookBottom;

    public AudioSource CraneAudio;
    private bool playAudio = false;

    private void FixedUpdate()
    {
        // toggle playAudio on fixed update
        if (playAudio)
        {
            playAudio = false;

        }
        else
        {
            // if we have not refreshed playAudio, stop the audio clip
            CraneAudio.Stop();
        }
    }

    /// <summary>
    /// Rotate the crane by a given angle.
    /// </summary>
    /// <param name="angle"></param>
    public void Rotate(float angle)
    {
        // start playing audio if not already
        if (!CraneAudio.isPlaying)
        {
            CraneAudio.Play();
        }
        playAudio = true;

        cabin.Rotate(new Vector3(0, 0, angle), Space.Self);
    }

    /// <summary>
    /// Translates the hook truck along the crane, within the given constraints.
    /// </summary>
    /// <param name="speed">the amount by which to move the truck</param>
    public void MoveTruck(float speed)
    {
        // start playing audio if not already
        if (!CraneAudio.isPlaying)
        {
            CraneAudio.Play();
        }
        playAudio = true;

        carriage.Translate(new Vector3(speed, 0, 0), Space.Self);
        if (carriage.localPosition.x > carriageInner.localPosition.x)
        {
            carriage.localPosition = carriageInner.localPosition;
        }

        if (carriage.localPosition.x < carriageOuter.localPosition.x)
        {
            carriage.localPosition = carriageOuter.localPosition;
        }
    }

    /// <summary>
    /// Move the crane hook vertically, within the given height constraints.
    /// </summary>
    /// <param name="speed">the amount by which to move the truck</param>
    public void MoveHook(float speed)
    {
        // start playing audio if not already
        if (!CraneAudio.isPlaying)
        {
            CraneAudio.Play();
        }
        playAudio = true;

        hook.Translate(new Vector3(0, 0, speed), Space.Self);
        if (hook.localPosition.z > hookTop.localPosition.z)
        {
            hook.localPosition = hookTop.localPosition;
        }
        if (hook.localPosition.z < hookBottom.localPosition.z)
        {
            hook.localPosition = hookBottom.localPosition;
        }
    }
}
