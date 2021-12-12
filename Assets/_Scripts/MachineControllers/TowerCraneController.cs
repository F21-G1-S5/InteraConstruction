using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCraneController : MonoBehaviour
{

    [SerializeField] private TowerCrane crane;
    [SerializeField] private float rotateSpeed = 1f;
    [SerializeField] private float truckSpeed = 1f;
    [SerializeField] private float hookSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // horizontal movement of the hook truck
        if (Input.GetKey(KeyCode.W))
        {
            crane.MoveTruck(-truckSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            crane.MoveTruck(truckSpeed * Time.deltaTime);
        }

        // vertical movement of the hook
        if (Input.GetKey(KeyCode.Z))
        {
            crane.MoveHook(-hookSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.C))
        {
            crane.MoveHook(hookSpeed * Time.deltaTime);
        }

        // rotation of the crane
        if (Input.GetKey(KeyCode.A))
        {
            crane.Rotate(-rotateSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            crane.Rotate(rotateSpeed * Time.deltaTime);
        }
    }
}
