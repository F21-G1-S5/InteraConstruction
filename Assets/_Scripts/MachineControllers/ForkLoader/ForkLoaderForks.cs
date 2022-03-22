using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class ForkLoaderForks handles movement of the "forks or blades" on the front part of the machine.
/// </summary>
public class ForkLoaderForks : MonoBehaviour
{
    [SerializeField] GameObject forksGo;
    [SerializeField] float height = 0;
    [SerializeField] float maxLift = 1; // max angle pos
    [SerializeField] Transform root; // initial pos

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
}
