using System;
using System.Collections;
using System.Collections.Generic;
using Sounds;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SoundsController))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float runSpeed = 2.3f;

    [SerializeField]
    private float walkSpeed = 1f;

    [SerializeField]
    private float movementTransitionSpeed = 8f;

    [SerializeField]
    private float _gravity = 11f;

    [SerializeField]
    private float smoothTime = 0.03f;
    private float _currentSpeed = 0.0f;
    [SerializeField]
    private SoundsController _soundsController;
    private CharacterController _characterController;
    private Animator _animator;
    private Vector3 _moveDirection;
    private Vector3 _velocity;
    private float _currentVelocity;
    private bool _isSitting = false;
    private bool _isFlipped = false;

    void Awake()
    {
        _soundsController = GetComponent<SoundsController>();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        float x = 0, z = 0;
        if (!_isFlipped)
        {
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
        }
        else
        {
            x = -Input.GetAxis("Horizontal");
            z = -Input.GetAxis("Vertical");
        }
        if (Input.GetKeyDown(KeyCode.Space))
            _isFlipped = !_isFlipped;
        _moveDirection = new Vector3(x, 0.0f, z);
    }

    void FixedUpdate()
    {
        if (!_isSitting)
        {
            Movement(_moveDirection);
            Rotation(_moveDirection);
            GravityMovement(_characterController.isGrounded);
        }
        if (_moveDirection == Vector3.zero)
            StartCoroutine(Sitting());
    }

    void Movement(Vector3 direction) {
        //_currentSpeed = Mathf.SmoothStep(_currentSpeed, walkSpeed, 2 * Time.deltaTime);
        if (direction != Vector3.zero && _animator.GetBool("IsWalking") && Input.GetKey(KeyCode.LeftShift))
        {
            _currentSpeed = Mathf.SmoothStep(_currentSpeed, runSpeed, movementTransitionSpeed * Time.deltaTime);
            _animator.SetBool("IsRunning", true);
            if (!_soundsController.AudioSource.isPlaying)
            {
                _soundsController.PlaySound(_soundsController.sounds[1], volume: 0.5f);
            }
            
        }
        else if (direction != Vector3.zero)
        {
            _currentSpeed = Mathf.SmoothStep(_currentSpeed, walkSpeed, movementTransitionSpeed * Time.deltaTime);
            _animator.SetBool("IsRunning", false);
            _animator.SetBool("IsWalking", true);
            if (!_soundsController.AudioSource.isPlaying)
            {
                _soundsController.PlaySound(_soundsController.sounds[0], volume: 0.5f);
            }
           
        }
        else
        {
            _currentSpeed = Mathf.SmoothStep(_currentSpeed, 0, movementTransitionSpeed * Time.deltaTime);
            _animator.SetBool("IsRunning", false);
            _animator.SetBool("IsWalking", false);
        }
            

        _characterController.Move(direction * (_currentSpeed * Time.deltaTime));
    }

    IEnumerator Sitting()
    {
        bool isAnimSitting = _animator.GetBool("IsSitting");
        if (Input.GetKey(KeyCode.C) && !isAnimSitting)
        {
            _isSitting = true;
            _animator.SetBool("IsSitting", true);
            _soundsController.PlaySound(_soundsController.sounds[2], volume: 0.5f);
        }
        else if (Input.anyKey && !Input.GetKey(KeyCode.C) && _isSitting && isAnimSitting)
        {
            _animator.SetBool("IsSitting", false);
            _currentSpeed = 0;
            yield return new WaitForSeconds(0.6f);
            _isSitting = false;
            _soundsController.PlaySound(_soundsController.sounds[3], volume: 0.5f);
        }
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
        if (direction == Vector3.zero) return;
        var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
    }

    
}
