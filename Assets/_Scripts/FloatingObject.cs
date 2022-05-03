using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float magnitude = 1f;

    float timer = 0;

    // Update is called once per frame
    void Update()
    {
        timer += speed * Time.deltaTime % (Mathf.PI * 2);

        transform.localPosition = new Vector3(0, Mathf.Sin(timer) * magnitude, 0);
    }
}
