using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour{
    private float speed = 0.05f;
    private float gravityMultiplier = 0.007f;
    private float yVel = 0f;
    private CharacterController cc;
    private readonly float gravity = Physics.gravity.y;
    private void Start() {
        cc = GetComponent<CharacterController>();
    }
    private void Update() {
        MovePlayer();
    }
    private void MovePlayer() {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal")*speed * Time.deltaTime, yVel, Input.GetAxisRaw("Vertical")*speed * Time.deltaTime).normalized;
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.z;
        if (cc.isGrounded && gravity < 0)
            yVel = 0f;
            yVel += -(gravityMultiplier);
        move.y = yVel;
        cc.Move(move);
    }
}
