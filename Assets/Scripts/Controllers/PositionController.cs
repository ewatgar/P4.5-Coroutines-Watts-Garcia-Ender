using System;
using UnityEngine;

public class PositionController : MonoBehaviour
{
    private CharacterController characterController;
    public float speed = 8;
    float vy = -10;
    public float jumpInitialSpeed = 3.5f;
    public float fallSpeedLimit = 20;
    public float gravity = -10;
    private bool isGrounded = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        updateMovementXZ();
        updateJump();
    }

    private void updateMovementXZ()
    {
        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");

        characterController.Move(transform.TransformDirection(new Vector3(
            xMovement,
            0,
            zMovement
        ) * speed * Time.deltaTime));
    }

    private void updateJump()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded){
            vy = jumpInitialSpeed;
        }else if (vy > -fallSpeedLimit){
            vy = vy + gravity*Time.deltaTime;
            if (vy < -fallSpeedLimit){
                vy = -fallSpeedLimit;
            }
        }

        characterController.Move(new Vector3(
            0,
            vy,
            0
        ) * speed/2 * Time.deltaTime);

        isGrounded = characterController.isGrounded;
    }
}
