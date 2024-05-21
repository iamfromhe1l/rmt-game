using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private Vector3 _offcet;
    [SerializeField] private float _smothing = 1f;
    [SerializeField] private float _fly_in_smothing = 0.5f;
    [SerializeField] private float xRotation = 30f;
    [SerializeField] private float zDistance = 10f;
    [SerializeField] public float yRotation;
    [SerializeField] private bool startRotationImmediately = false;
    private Vector3 _originalOffset;
    private bool _isFirstOccurrence = false;
    public bool isFirstOccurrence => _isFirstOccurrence;
    private bool _isFlipped = false;


    private void Start()
    {
        Quaternion rotation = Quaternion.Euler(0, yRotation, 0);
        _originalOffset = rotation * _offcet;
    }

    private void FixedUpdate()
    {
        Move();
    }
    
    public void SetTarget(Transform target)
    {
        _targetTransform = target;
    }
    
    private void Move()
    {
        var position = _targetTransform.position;
        var whichSmoothing = _isFirstOccurrence ? _smothing : _fly_in_smothing;
        Vector3 nextPosition = Vector3.Lerp(transform.position,
            new Vector3(
                position.x +  (_isFirstOccurrence ? _offcet.x : _originalOffset.x),
                position.y + _originalOffset.y,
                position.z + (_isFirstOccurrence ? _offcet.z : _originalOffset.z)),
                Time.fixedDeltaTime * whichSmoothing);
        transform.position = nextPosition;
    }

    private Vector3 _originalPosition;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            if (!_isFlipped)
            {
                _originalPosition = transform.position;
                Quaternion rotation = Quaternion.Euler(0, yRotation, 0);
                Vector3 offset = rotation * new Vector3(0, 0, zDistance);
                transform.position = _originalPosition + offset;
                transform.eulerAngles = new Vector3(xRotation, yRotation + 180, 0);
                _offcet = -_originalOffset;
                _isFlipped = true;
            }
            else
            {
                transform.position = _originalPosition;
                transform.eulerAngles = new Vector3(xRotation, yRotation, 0);
                _offcet = _originalOffset;
                _isFlipped = false;
            }
        }
        if (!_isFirstOccurrence && (startRotationImmediately || Vector3.Distance(transform.position, _targetTransform.position) <= zDistance))
        {
            Quaternion targetRotation = Quaternion.Euler(new Vector3(xRotation, yRotation, 0));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _fly_in_smothing);
            if (Quaternion.Angle(transform.rotation, targetRotation) == 0f)
            {
                _isFirstOccurrence = true;
                Debug.Log("Enter");
            }
        }
    }
}