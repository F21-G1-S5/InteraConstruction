using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulldozerMovement : MonoBehaviour
{
    float speed = 5.0f;
    float angularSpeed = 45.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CameraSwitching.playerInVehicle)
        {
            var h = Input.GetAxisRaw("Horizontal");
            var v = Input.GetAxisRaw("Vertical");
            transform.Rotate(0, h * angularSpeed * Time.deltaTime, 0);
            transform.Translate(0, 0, v * speed * Time.deltaTime);
        }
    }
}
