using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Transform mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main?.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCamera != null)
        {
            Vector3 cameraDir = Camera.main.transform.forward;
            cameraDir.y = 0;

            transform.rotation = Quaternion.LookRotation(cameraDir);
        }
    }

    private void LateUpdate()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main.transform;
        }
    }
}
