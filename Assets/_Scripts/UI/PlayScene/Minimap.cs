using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tracks the given transform while maintaining the same y-position.
/// </summary>
public class Minimap : MonoBehaviour
{
    public Transform player;

    // LateUpdate gets called after the regular Update phase
    private void LateUpdate()
    {
        Vector3 newPos = player.position;
        newPos.y = transform.position.y;
        transform.position = newPos;

        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
