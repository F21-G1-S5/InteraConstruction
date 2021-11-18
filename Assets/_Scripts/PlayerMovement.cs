using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour{
    [SerializeField]
    private float speedMultiplier = 6f;
    [SerializeField]
    private float gravityMultiplier = 2f;
    private float yVelocity = 0f;
    private CharacterController cc;
    private const float GRAVITY_REDUCTION = 1000f;
    private readonly float gravity = Physics.gravity.y;
    private void Start() {
        cc = GetComponent<CharacterController>();
    }
    private void Update() {
        MovePlayer();
    }
    private void MovePlayer() {
        Vector3 movementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0,Input.GetAxisRaw("Vertical")).normalized;
        Vector3 movement = new Vector3(movementInput.x*speedMultiplier,yVelocity*(gravityMultiplier/GRAVITY_REDUCTION),movementInput.z*speedMultiplier);
        if (cc.isGrounded && gravity < 0)
            yVelocity = 0f;
        else
            yVelocity += -(gravity*gravity);
        cc.Move(movement*Time.deltaTime);
    }
}
