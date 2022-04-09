using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class ForkLoaderForks handles movement of the "forks or blades" on the front part of the machine.
/// </summary>
public class ForkLoaderForks : MonoBehaviour
{
    [SerializeField] GameObject forksGo;
    [SerializeField] float weightLimit;
    [SerializeField] float height = 0;
    [SerializeField] float maxLift = 1; // max angle pos
    [SerializeField] Transform root; // initial pos

    public AudioSource LiftAudio;
    public AudioSource startUpAudio;
    public AudioSource idleAudio;

    private GameObject pickObj;
    private bool pickUp;

    void Start()
    {
        //forksPos = forksGo.transform.position.y;
        Lift(height);
    }

 
    void Update()
    {
        
    }

    public void Lift(float amount)
    {
        height += amount;
        if (height > maxLift)
        {
            height = maxLift;
        }
        else
        {
            forksGo.transform.position = new Vector3(
                forksGo.transform.position.x,
                root.position.y + height,
                forksGo.transform.position.z);
        }

    }

    public void Lower(float amount)
    {
        height -= amount;
        if (height < 0)
        {
            height = 0;
        }
        else
        {
            forksGo.transform.position = new Vector3(
                forksGo.transform.position.x,
                root.position.y + height,
                forksGo.transform.position.z);
        }

    }

    /// <summary>
    /// Pick up item if a valid GameObject is colliding with the Pickup object
    /// </summary>
    public void PickUpItem() {
        if(pickUp) {
            pickObj.GetComponent<Rigidbody>().isKinematic = true;
            pickObj.transform.parent = this.gameObject.transform;
        }
    }

    /// <summary>
    /// Release the GameObject currently picked up by this pickup object
    /// </summary>
    public void PutDownItem() {
        if(pickObj == null) {
            return;
        }
        pickUp = false;
        pickObj.transform.parent = null;
        pickObj.GetComponent<Rigidbody>().isKinematic = false;
        // temporary fix for object parenting bug in multiplayer
        pickObj.transform.position = gameObject.transform.position;
        pickObj.transform.rotation = gameObject.transform.rotation;
    }

    /// <summary>
    /// Move items tagged with "PickUpItem" with Forklift.
    /// </summary>
    /// <param name="collision">The collision that is taking place.</param>
    private void OnTriggerStay(Collider other) {
        if(other.gameObject.CompareTag("PickUpItem")) {
            if(other.gameObject.GetComponent<Rigidbody>().mass <= weightLimit) {
                pickObj = other.gameObject;
                pickUp = true;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("PickUpItem")) {
            pickUp = false;
            pickObj = null;
        }
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
