using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class Indicator : MonoBehaviour
{
    public Transform posA, posB;
    public float speed;
    public Transform startPos;

    Vector3 nextPos;
  
    void Start()
    {
        nextPos = startPos.position;
    }

    
    void Update()
    {
        if(transform.position == posA.position)
        {
            nextPos = posB.position;
        }
        if(transform.position == posB.position)
        {
            nextPos = posA.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(posA.position, posB.position);
    }
}
