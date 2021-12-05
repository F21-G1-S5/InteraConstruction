using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        CraneAudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //Rotate(4f * Time.deltaTime);
        //MoveTruck(1f * Time.deltaTime);
        //MoveHook(-1f * Time.deltaTime);
    }

    public void Rotate(float angle)
    {
        cabin.Rotate(new Vector3(0, 0, angle), Space.Self);
    }

    public void MoveTruck(float speed)
    {
       
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

    public void MoveHook(float speed)
    {
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
