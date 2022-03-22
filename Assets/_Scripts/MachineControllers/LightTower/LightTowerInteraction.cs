using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class <c>LightTowerInteraction</c> handles interaction regarding light enabling
/// Attach this MonoBehaviour as a component to an object to enable these controls.
/// </summary>
public class LightTowerInteraction : MonoBehaviour, InteractiveMachine
{
    private bool lightOn = false;
    public Material[] materials;
    public GameObject light;
    public KeyCode activator;
    public GameObject target;

    Renderer renderer;

    void Start()
    {

        renderer = GetComponent<Renderer>();
        renderer.enabled = true;
        renderer.sharedMaterial = materials[0]; //initiate start up default material
    }

    
    void Update()
    {
        
    }

    /// <summary>
    /// When the player presses the interact key, call this function
    /// </summary>
    /// <param name="player">the object trying to interact</param>
    /// <returns>Returns a reference to the machine, or null if the machine is being operated by someone else</returns>
    public InteractiveMachine StartInteraction(GameObject player)
    {
        //if light is off, then turn it on by changing material
        if (lightOn == false)
        {
            // Set the new material on the GameObject
            renderer.sharedMaterial = materials[1];
            // turn lightning for spotlight on
            light.SetActive(true);

            lightOn = true;

        } //if light is on, then turn off on by changing material
        else if (lightOn == true)
        {
            // Set the new material on the GameObject
            renderer.sharedMaterial = materials[0];
            // turn lightning for spotlight off
            light.SetActive(false);

            lightOn = false;
        }
        return null;
    }

    /// <summary>
    /// End the interaction
    /// </summary>
    /// <param name="player">the object trying to leave the machine</param>
    public void EndInteraction(GameObject player)
    {
        // not needed for this machine, since we don't do anything to the player object
        return;
    }

    /// <summary>
    /// Calls the machine's own Update function, allowing it to react to user input and perform other
    /// "operations" while a player is using it.
    /// </summary>
    public void Operate()
    {
        // same as above
        return;
    }
}
