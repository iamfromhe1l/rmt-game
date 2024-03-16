using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraMove : MonoBehaviour
{
    [SerializeField ,Range(1, 20)] private float DistanceFromPlayerToX_Axis;
    [SerializeField, Range(1,20)] private float DistanceFromPlayerToY_Axis;
    [SerializeField] private Transform _playerPosition;
    [SerializeField, Range(1, 20)] float _cameraHeight;
    private Transform _cameraPosition;
    private Vector3 _oldPositionPlayer;
    private Window _window;
    class Point
    {
        public float x { get; set; }
        public float y { get; set; }
        public Point(float x=0, float y=0)
        {
            this.x = x; this.y = y;
        }
        public Point SumPoint(Point x ,Point y)
        {
            return new Point(x.x + x.y, y.x + y.y);
        }
    }
    class Window
    {
        public Point _lowerLeftCorner { get; set;}
        public Point _upperRightCorner { get; set; }
        public Window(float DistanceFromPlayerToX_Axis, float DistanceFromPlayerToY_Axis,Transform transform)
        {
            _lowerLeftCorner = new Point();
            _upperRightCorner = new Point();
            _lowerLeftCorner.x = transform.position.x  - DistanceFromPlayerToX_Axis;
            _lowerLeftCorner.y = transform.position.z - DistanceFromPlayerToY_Axis;
            _upperRightCorner.x = transform.position.x + DistanceFromPlayerToX_Axis;
            _upperRightCorner.y = transform.position.z + DistanceFromPlayerToY_Axis;
        }
        public bool InWindow(Transform transform)
        {
            if (_lowerLeftCorner.x < transform.position.x  && transform.position.x < _upperRightCorner.x &&
                _lowerLeftCorner.y < transform.position.z && transform.position.z < _upperRightCorner.y)
                return true;
            return false;
        }
    }
    void MoveCamera()
    {
        Vector3 deltaPosition = _playerPosition.position - _oldPositionPlayer;
        if (!_window.InWindow(_playerPosition))
        {
            _window._lowerLeftCorner.x += deltaPosition.x;
            _window._lowerLeftCorner.y += deltaPosition.z;
            _window._upperRightCorner.x += deltaPosition.x;
            _window._upperRightCorner.y += deltaPosition.z;
            _cameraPosition.position += deltaPosition;
        }
        _cameraPosition.position = new Vector3 (_cameraPosition.position.x,
            _playerPosition.position.y + _cameraHeight,
            _cameraPosition.position.z);
        _oldPositionPlayer = _playerPosition.position;
    }
    private void Awake(){
        _cameraPosition = GetComponent<Transform>();
    }
    private void Start()
    {

        _oldPositionPlayer = _playerPosition.position;
        _window = new Window(DistanceFromPlayerToX_Axis, DistanceFromPlayerToY_Axis,_playerPosition);
    }
    private void LateUpdate() 
    {
        MoveCamera();
        Debug.Log("");
    }
} 
