using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class <c>LightTowerInteraction</c> handles interaction regarding light enabling
/// Attach this MonoBehaviour as a component to an object to enable these controls.
/// </summary>
public class LightTowerInteraction : MonoBehaviour
{
    private bool lightOn = false;
    public Material[] materials;
    public GameObject light;

    Renderer renderer;

    void Start()
    {

        renderer = GetComponent<Renderer>();
        renderer.enabled = true;
        renderer.sharedMaterial = materials[0];

    }

    
    void Update()
    {
        //if light is off, then turn it on by changing material
        if (lightOn == false && Input.GetKeyDown(KeyCode.Alpha4))
        {
            // Set the new material on the GameObject
            renderer.sharedMaterial = materials[1];
            // turn lightning for spotlight on
            light.SetActive(true);

            lightOn = true;

        } //if light is on, then turn off on by changing material
        else if(lightOn == true && Input.GetKeyDown(KeyCode.Alpha4))
        {
            // Set the new material on the GameObject
            renderer.sharedMaterial = materials[0];
            // turn lightning for spotlight off
            light.SetActive(false);

            lightOn = false;
        }
    }
}
