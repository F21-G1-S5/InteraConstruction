using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCraneHook : MonoBehaviour{
    private float hookSpeed=0.1f;
    private float hookLowerSpeed=0.1f;
    private float craneRotateSpeed=0.3f;
    private Transform hookBase;
    private Transform hook;
    private Transform craneBase;

    private void Start() {
        hookBase=transform.Find("towerCrane_cabine/point_0/point_cabine/point_truck").transform;
        hook=transform.Find("towerCrane_cabine/point_0/point_cabine/point_truck/point_hook").transform;
        craneBase=transform.Find("towerCrane_cabine/point_0/point_cabine").transform;
    }

    private void FixedUpdate() {
        /*Tests
        if (Input.GetKey(KeyCode.UpArrow))
            MoveHookUp();
        if (Input.GetKey(KeyCode.DownArrow))
            MoveHookDown();
        if(Input.GetKey(KeyCode.LeftArrow))
            MoveHookForward();
        if (Input.GetKey(KeyCode.RightArrow)) 
            MoveHookBackward();
        if(Input.GetKey(KeyCode.C))
            RotateCraneCW();
        if(Input.GetKey(KeyCode.V))
            RotateCraneCCW();
        */
    }

    private void RotateCraneCW() {
        craneBase.Rotate(new Vector3(0,0,craneRotateSpeed));
    }

    private void RotateCraneCCW() {
        craneBase.Rotate(new Vector3(0,0,-craneRotateSpeed));
    }
    
    private void MoveHookForward() {
        hookBase.localPosition=new Vector3(Mathf.Clamp(hookBase.localPosition.x-hookSpeed,-34f,-4f),hookBase.localPosition.y,hookBase.localPosition.z);
    }

    private void MoveHookBackward() {
        hookBase.localPosition=new Vector3(Mathf.Clamp(hookBase.localPosition.x+hookSpeed,-34f,-4f),hookBase.localPosition.y,hookBase.localPosition.z);
    }

    private void MoveHookDown() {
        hook.localPosition=new Vector3(hook.localPosition.x,hook.localPosition.y,Mathf.Clamp(hook.localPosition.z-hookLowerSpeed,-50,-1.772861f));
    }

    private void MoveHookUp() {
        hook.localPosition=new Vector3(hook.localPosition.x,hook.localPosition.y,Mathf.Clamp(hook.localPosition.z+hookLowerSpeed,-50,-1.772861f));
    }
}
