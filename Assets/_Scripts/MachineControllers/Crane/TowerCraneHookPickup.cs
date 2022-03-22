using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <c>TowerCraneHookPickup</c> is a component that may pickup or put down a single object at a time
/// as long as that object weighs less than the given weight limit
/// </summary>
public class TowerCraneHookPickup : MonoBehaviour {
    public float weightLimit;
    private GameObject pickObj;
    public bool pickUp;

    private void Start() {
        weightLimit = 100f;
    }

    /// <summary>
    /// Pick up item if a valid GameObject is colliding with the Pickup object
    /// </summary>
    public void PickUpItem() {
        if (pickUp) {
            pickObj.GetComponent<Rigidbody>().isKinematic = true;
            pickObj.transform.parent = this.gameObject.transform;
        }
    }

    /// <summary>
    /// Release the GameObject currently picked up by this pickup object
    /// </summary>
    public void PutDownItem() {
        pickUp = false;
        pickObj.transform.parent = null;
        pickObj.GetComponent<Rigidbody>().isKinematic = false;
        // temporary fix for object parenting bug in multiplayer
        pickObj.transform.position = gameObject.transform.position;
        pickObj.transform.rotation = gameObject.transform.rotation;
    }

    /// <summary>
    /// Move items tagged with "PickUpItem" with crane.
    /// </summary>
    /// <param name="collision">The collision that is taking place.</param>
    public void OnTriggerStay(Collider collision) {
        if (collision.gameObject.CompareTag("PickUpItem")) {
            if (collision.gameObject.GetComponent<Rigidbody>().mass <= weightLimit) {
                pickObj = collision.gameObject;
                pickUp = true;
            }
        }
    }

    public void OnTriggerExit(Collider collision) {
        if (collision.gameObject.CompareTag("PickUpItem")) {
            pickUp = false;
        }
    }
}
