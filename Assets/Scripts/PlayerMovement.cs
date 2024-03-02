using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    public float Speed = 2f;

    private CharacterController controller;
    private bool groundedPlayer;
    private Vector3 playerVelocity;
    private float gravityValue = -9.81f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveH, 0.0f, moveV);

        controller.Move(movement * Speed * Time.deltaTime);

        if (movement != Vector3.zero)
        {
            gameObject.transform.forward = movement;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
