using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScissorLiftLifting : MonoBehaviour{
    private float liftSpeed = 0.01f;
    private Transform platform;
    private Animator animator;

    private void Start() {
        platform=transform.Find("top").transform;
        animator=GetComponent<Animator>();
    }

    private void FixedUpdate() {
        /*
        if (Input.GetKey(KeyCode.UpArrow)) {
            LiftUp();
        } else {
           
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            LiftDown();
        } else {
            
        }
        */
    }

    private void LiftUp() {
        ScissorLift.Lift();
        platform.localPosition=new Vector3(platform.localPosition.x,platform.localPosition.y+liftSpeed,platform.localPosition.z);
    }

    private void LiftDown() {
        ScissorLift.Lower();
        platform.localPosition=new Vector3(platform.localPosition.x,platform.localPosition.y+liftSpeed,platform.localPosition.z);
    }
}
