using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DozerBlade : MonoBehaviour
{
    [SerializeField] Transform bladePivot;
    [SerializeField] float maxLift = 1; // max angle rads
    [SerializeField] float lift = 0; // initial angle in radss

    //public AudioSource bulldozerAudio;
    public AudioSource LiftAudio;

    private float rootAngle;

    private void Start()
    {
        
        rootAngle = bladePivot.localRotation.x;
        Lift(lift);
    }

    private void Update()
    {
        //if (Input.GetAxis("Vertical") > 0)
        //{
        //    Lift(1f * Time.deltaTime);
        //}
    }

    public void Lift(float amount)
    {
        lift += amount;
        if (lift > maxLift) lift = maxLift;
        Quaternion bladeAngle = bladePivot.localRotation;
        bladeAngle.x = rootAngle - lift;
        bladePivot.localRotation = bladeAngle;
    }

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
}
