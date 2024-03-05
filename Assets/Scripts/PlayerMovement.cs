using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;

    [SerializeField]
    private float _gravity = 11f;

    [SerializeField]
    private float smoothTime = 0.05f;

    private CharacterController _characterController;
    private Vector3 _moveDirection;
    private Vector3 _velocity;
    private float _currentVelocity;

    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        _moveDirection = new Vector3(x, 0.0f, z);
    }

    void FixedUpdate()
    {
        Movement(_moveDirection);
        GravityMovement(_characterController.isGrounded);
        Rotation(_moveDirection);
    }

    void Movement(Vector3 direction) {
        _characterController.Move(direction * _speed * Time.deltaTime);
    }

    void GravityMovement(bool isGrounded)
    {
        if (isGrounded && _velocity.y < 0.0f)
            _velocity.y = -1f;
        else
        {
            _velocity.y -= _gravity * Time.fixedDeltaTime;
            _characterController.Move(_velocity * Time.fixedDeltaTime);
        }
    }

    void Rotation(Vector3 direction)
    {
        var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
    }
}
