using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 5.0f;
    public Transform playerBody;
    public PlayerMovement player;

    private float XRotation = 0.0f;

    [Header("Headbob parameters")]
    [SerializeField] private float walkingHeadBobSpeed = 14f;
    [SerializeField] private float walkingHeadBobAmount = 0.05f;
    private float defaultposY;
    private float timer;
    private Camera camera;
    private bool canUseHeadBob;

    // Start is called before the first frame update
    void Awake()
    {
        camera = this.GetComponent<Camera>();
        defaultposY = camera.transform.localPosition.y; //get default camera position
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        XRotation -= mouseY;
        XRotation = Mathf.Clamp(XRotation, -90.0f, 90.0f);

        transform.localRotation = Quaternion.Euler(XRotation, 0.0f, 0.0f);
        playerBody.Rotate(Vector3.up * mouseX);

        HandleHeadBob();

        if (Pause_Resume.gamePaused)
        {
            mouseSensitivity = 0.0f;
        }
        else
        {
            mouseSensitivity = 5.0f;
        }

    }

    /// <summary>
    /// HandleHeadBob handles dynamic movement of the camera to simulate head bob while walking
    /// </summary>
    private void HandleHeadBob()
    {
        //if player moves
        if (player.move.z != 0)
        {
            timer += Time.deltaTime * walkingHeadBobSpeed;

            //transform Y pos according to default pos. The sin gives a value between -1 & 1, if timer is negative lower camera, else raise it.
            camera.transform.localPosition = new Vector3(
                camera.transform.localPosition.x,
                defaultposY + Mathf.Sin(timer) * walkingHeadBobAmount, 
                camera.transform.localPosition.z);
        }
    }
}
