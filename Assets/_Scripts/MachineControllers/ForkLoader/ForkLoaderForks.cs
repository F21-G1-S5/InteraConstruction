using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class ForkLoaderForks handles movement of the "forks or blades" on the front part of the machine.
/// </summary>
public class ForkLoaderForks : MonoBehaviour
{
    [SerializeField] GameObject forksGo;
    [SerializeField] float forksPos;
    [SerializeField] float maxLift = 1; // max angle pos
    [SerializeField] float lift = 0; // initial pos

    void Start()
    {
        //forksPos = forksGo.transform.position.y;
        Lift(lift);
    }

 
    void Update()
    {
        
    }

    public void Lift(float amount)
    {
        forksPos = forksGo.transform.position.y;
        lift += amount;
        if (lift > maxLift) 
        {
            lift = maxLift;
        }
        else
        {
            forksPos += lift;
        }
            
    }
}
