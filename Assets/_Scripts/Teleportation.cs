using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// WIP
public class Teleportation : MonoBehaviour
{
    private Transform cp1;
    private Transform cp2;
    private Transform cp3;
    private Transform cp4;
    private void Start() {
        cp1 = GameObject.Find("Checkpoint1").transform;
        cp2 = GameObject.Find("Checkpoint2").transform;
        cp3 = GameObject.Find("Checkpoint3").transform;
        cp4 = GameObject.Find("Checkpoint4").transform;
    }
    public void TeleportToCheckpoint1() {
        transform.position = cp1.position;
    }
    public void TeleportToCheckpoint2() {
        transform.position = cp2.position;
    }
    public void TeleportToCheckpoint3() {
        transform.position = cp3.position;
    }
    public void TeleportToCheckpoint4() {
        transform.position = cp4.position;
    }
}
