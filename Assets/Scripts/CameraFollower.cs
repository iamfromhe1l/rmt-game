using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private Vector3 _offcet;
    [SerializeField] private float _smothing = 1f;
    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        Vector3 nextPosition = Vector3.Lerp(transform.position, _targetTransform.position + _offcet, Time.fixedDeltaTime * _smothing);
        transform.position = nextPosition;
    }
}
