using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// serializable data for the player.
// use this class as the interface for sending and receiving data to/from the save system
[System.Serializable]
public class PlayerData
{
    // player position
    public float[] position;

    // player rotation
    public float[] rotation;

    // TODO: when someone creates the player controller, we need to change the parameter type from 'GameObject' to the more
    //       specific player object.
    public PlayerData(GameObject go)
    {
        position = new float[3];
        position[0] = go.transform.position.x;
        position[1] = go.transform.position.y;
        position[2] = go.transform.position.z;

        rotation = new float[4];
        rotation[0] = go.transform.localRotation.x;
        rotation[1] = go.transform.localRotation.y;
        rotation[2] = go.transform.localRotation.z;
        rotation[3] = go.transform.localRotation.w;
    }

    // helpers for converting native c# objects to Unity objects
    public Vector3 GetPosition()
    {
        return new Vector3(position[0], position[1], position[2]);
    }

    public Quaternion GetRotation()
    {
        return new Quaternion(rotation[0], rotation[1], rotation[2], rotation[3]);
    }
}
