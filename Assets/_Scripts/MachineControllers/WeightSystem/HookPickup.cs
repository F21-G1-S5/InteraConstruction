using UnityEngine;

/// <summary>
/// Defines behavior for an object picking up another object from a the
/// grabbing object's root position, while taking the rigid body's mass
/// into account.
/// </summary>
public class HookPickup : MonoBehaviour
{
    public float weightLimit = 100f;
    private GameObject pickObj;
    public bool pickUp = false;

    /// <summary>
    /// attempt to grab the object being collided with
    /// </summary>
    public void PickUpItem()
    {
        if (pickUp)
        {
            pickObj.GetComponent<Rigidbody>().isKinematic = true;
            pickObj.transform.parent = this.gameObject.transform;
        }
    }

    /// <summary>
    /// release currently grabbed item
    /// </summary>
    public void PutDownItem()
    {
        if (pickUp)
        {
            pickUp = false;
            pickObj.transform.parent = null;
            pickObj.GetComponent<Rigidbody>().isKinematic = false;
            // temporary fix for object parenting bug in multiplayer
            // by re-setting the object's position, Photon triggers an update
            // to the clients
            pickObj.transform.position = gameObject.transform.position;
            pickObj.transform.rotation = gameObject.transform.rotation;
        }
    }

    // Unity's built-in collision detection
    public void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("PickUpItem"))
        {
            if (collision.gameObject.GetComponent<Rigidbody>().mass <= weightLimit)
            {
                pickObj = collision.gameObject;
                pickUp = true;
            }
        }
    }

    public void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("PickUpItem"))
        {
            pickUp = false;
            pickObj = null;
        }
    }
}
