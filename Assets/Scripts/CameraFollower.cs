using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private Vector3 _offcet;
    [SerializeField] private float _smothing = 1f;
    private Vector3 _originalOffset;
    private bool _isFlipped = false;
    private bool testFix = false;
    
    private void Start()
    { 
        _originalOffset = _offcet;
    }

    
    private void FixedUpdate()
    {
        if (!testFix)
        {
            Move();
        }
    }
    // private void Move()
    // {
    //     Vector3 nextPosition = Vector3.Lerp(transform.position, _targetTransform.position + _offcet, Time.fixedDeltaTime * _smothing);
    //     transform.position = nextPosition;
    // }
    
    private void Move()
    {
        Vector3 nextPosition = Vector3.Lerp(transform.position, new Vector3(_targetTransform.position.x + _offcet.x, _targetTransform.position.y + _originalOffset.y, _targetTransform.position.z + _offcet.z), Time.fixedDeltaTime * _smothing);
        transform.position = nextPosition;
    }

    private Vector3 _originalPosition;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            testFix = true;
            if (!_isFlipped)
            {
                _originalPosition = transform.position; // сохраняем исходное положение
                var vector3 = transform.position;
                vector3.z += 10; // сдвигаем по оси Z на 10
                transform.position = vector3;
                float currentXRotation = transform.eulerAngles.x;
                transform.eulerAngles = new Vector3(currentXRotation, transform.eulerAngles.y + 180, 0);
                _offcet = -_originalOffset;
                _isFlipped = true;
            }
            else
            {
                transform.position = _originalPosition; // возвращаемся к исходному положению
                float currentXRotation = transform.eulerAngles.x;
                transform.eulerAngles = new Vector3(currentXRotation, transform.eulerAngles.y - 180, 0);
                _offcet = _originalOffset;
                _isFlipped = false;
            }

            testFix = false;
        }
    }
}
