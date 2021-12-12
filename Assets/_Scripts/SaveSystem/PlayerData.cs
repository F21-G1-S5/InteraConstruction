using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>PlayerData</c> is a serializable set of primitive data representing a player in the game.
/// This data gets passed by the JSONSaveSystem, and can convert back to Vector3 and Quaternions.
/// </summary>
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

    /// <summary>
    /// method <c>GetPosition</c>
    /// </summary>
    /// <returns>Vector3 representing the object's position.</returns>
    public Vector3 GetPosition()
    {
        return new Vector3(position[0], position[1], position[2]);
    }

    /// <summary>
    /// method <c>GetRotation</c>
    /// </summary>
    /// <returns>Quaternion representing the object's rotation.</returns>
    public Quaternion GetRotation()
    {
        return new Quaternion(rotation[0], rotation[1], rotation[2], rotation[3]);
    }
}
