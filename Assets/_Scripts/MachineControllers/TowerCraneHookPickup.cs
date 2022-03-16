using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCraneHookPickup : MonoBehaviour {
    public float weightLimit;
    private GameObject pickObj;
    public bool pickUp;

    private void Start() {
        weightLimit = 100f;
    }

    public void PickUpItem() {
        if (pickUp) {
            pickObj.GetComponent<Rigidbody>().isKinematic = true;
            pickObj.transform.parent = this.gameObject.transform;
        }
    }

    public void PutDownItem() {
        pickUp = false;
        pickObj.transform.parent = null;
        pickObj.GetComponent<Rigidbody>().isKinematic = false;
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
