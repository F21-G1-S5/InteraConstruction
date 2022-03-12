using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines functions for triggering animations in the player's AnimationController.
/// Also has the option to read user inputs and trigger the animations itself.
/// The controller abstract the actual Animator's parameters. if you want to manually control this Animator
/// feel free to ignore or remove this component. This script is meant to serve as a guide using this animator.
/// </summary>
public class PeteAnimationController : MonoBehaviour
{
    public bool listenForInputs = false;
    [SerializeField] private bool isSitting = false;

    [SerializeField] private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }

        anim.SetBool("Sitting", isSitting);
    }

    // Update is called once per frame
    void Update()
    {
        if (listenForInputs)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Walking();
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                Idle();
            }
        }
    }

    /// <summary>
    /// Trigger Pete@Walk animation
    /// </summary>
    public void Walking()
    {
        anim.SetFloat("Speed", 0.5f);
        anim.SetBool("Sitting", false);
    }

    /// <summary>
    /// Trigger Pete@Jogging animation
    /// </summary>
    public void Jogging()
    {
        anim.SetFloat("Speed", 1.1f);
        anim.SetBool("Sitting", false);
    }

    /// <summary>
    /// Tigger Pete@Sitting animation
    /// </summary>
    public void Sitting()
    {
        anim.SetBool("Sitting", true);
    }

    /// <summary>
    /// Tigger Pete@Idle animation
    /// </summary>
    public void Idle()
    {
        anim.SetFloat("Speed", 0);
        anim.SetBool("Sitting", false);
    }

    /// <summary>
    /// Sets the Speed parameter of the animator to a given float value
    /// </summary>
    /// <param name="speed"></param>
    public void SetSpeed(float speed)
    {
        anim.SetFloat("Speed", speed);
    }

    /// <summary>
    /// Sets the Sitting state of the animator
    /// </summary>
    /// <param name="isSitting"></param>
    public void IsSitting(bool isSitting)
    {
        anim.SetBool("Sitting", isSitting);
    }
}
