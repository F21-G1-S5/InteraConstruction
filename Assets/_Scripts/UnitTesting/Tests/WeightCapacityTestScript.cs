using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class WeightCapacityTestScript
{
    /// <summary>
    /// test interaction where a machine attempts to pick up an object within its capacity
    /// using the "hook" behavior
    /// 
    /// expected outcome: the machine becomes the parent of the object's transform, gaining control over its position
    /// </summary>
    [Test]
    public void TestPickupUnderCapacity()
    {
        GameObject machine = new GameObject("test crane");
        HookPickup hook = machine.AddComponent<HookPickup>();
        hook.weightLimit = 10f;

        GameObject crate = new GameObject("test crate");
        BoxCollider collider = crate.AddComponent<BoxCollider>();
        Rigidbody rb = crate.AddComponent<Rigidbody>();
        rb.mass = 5f;
        crate.tag = "PickUpItem";

        // simulate collision conditions (since we don't have the game's runtime)
        hook.OnTriggerStay(collider);

        hook.PickUpItem();

        Assert.IsTrue(crate.transform.parent != null);
    }

    /// <summary>
    /// Test interaction where a machine releases control of an object it is currently carrying
    /// 
    /// expected outcome: the object's transform is no longer parented
    /// </summary>
    [Test]
    public void TestRelease()
    {
        GameObject machine = new GameObject("test crane");
        HookPickup hook = machine.AddComponent<HookPickup>();
        hook.weightLimit = 10f;

        GameObject crate = new GameObject("test crate");
        BoxCollider collider = crate.AddComponent<BoxCollider>();
        Rigidbody rb = crate.AddComponent<Rigidbody>();
        rb.mass = 5f;
        crate.tag = "PickUpItem";

        // manually attach the crate to the grabbing machine
        hook.OnTriggerStay(collider);
        rb.isKinematic = true;
        crate.transform.parent = machine.transform;

        // attempt to release it
        hook.PutDownItem();

        Assert.IsTrue(crate.transform.parent == null);
    }

    /// <summary>
    /// test interaction where a machine attempts to pick up an object with greater mass than its capacity
    /// using the "hook" behavior
    /// 
    /// expected outcome: the machine does not gain control of the object's transform component
    /// </summary>
    [Test]
    public void TestPickupOverCapacity()
    {
        GameObject machine = new GameObject("test crane");
        HookPickup hook = machine.AddComponent<HookPickup>();
        hook.weightLimit = 10f;

        GameObject crate = new GameObject("test crate");
        BoxCollider collider = crate.AddComponent<BoxCollider>();
        Rigidbody rb = crate.AddComponent<Rigidbody>();
        rb.mass = 15f; // mass is over capacity this time
        crate.tag = "PickUpItem";

        hook.OnTriggerStay(collider);
        hook.PickUpItem();

        Assert.IsTrue(crate.transform.parent == null);
    }
}
