using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulldozerMovement : MonoBehaviour
{
    float speed = 5.0f;
    float angularSpeed = 45.0f;
    [SerializeField] DozerBlade dozerBlade;
    [SerializeField] float bladeSpeed = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        if (dozerBlade == null)
        {
            dozerBlade = GetComponent<DozerBlade>();
        }
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

            var raise = Input.GetAxis("Fire1");
            var lower = Input.GetAxis("Fire2");
            if (raise > 0)
            {
                dozerBlade.Lift(bladeSpeed * Time.deltaTime);
                dozerBlade.PlayLiftAudio();
            }
            else if (lower > 0)
            {
                dozerBlade.Lower(bladeSpeed * Time.deltaTime);
                dozerBlade.PlayLiftAudio();
            }
            else
            {
                dozerBlade.StopLiftAudio();
            }
        }
    }
}
