using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a simple character controller for the sample scene

public class ProgressSampleCharacterController : MonoBehaviour
{
    [SerializeField] private float speed = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 movement = (new Vector3(x, 0, z)).normalized;

        transform.position = transform.position + movement * speed * Time.deltaTime;
    }
}
