using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraMove : MonoBehaviour
{
    [SerializeField ,Range(1, 20)] private float DistanceFromPlayerTo2Edge;
    [SerializeField, Range(1,20)] private float DistanceFromPlayerTo1Edge;
    [SerializeField] private Transform _playerPosition;
    [SerializeField, Range(1, 20)] float _cameraHeight;
    private Vector3 _cameraPosition;
    private Vector3 _oldPositionPlayer;
    private Window _window;
    class Point
    {
        public float x { get; set; }
        public float y { get; set; }
        public Point(float x, float y)
        {
            this.x = x; this.y = y;
        }
        protected Point SumPoint(Point x ,Point y)
        {
            return new Point(x.x + x.y, y.x + y.y);
        }
    }
    class Window
    {
        public Point _lowerLeftCorner { get; set;}
        public Point _upperRightCorner { get; set; }
        public Window(float DistanceFromPlayerTo1Edge, float DistanceFromPlayerTo2Edge,Transform transform)
        {
            _lowerLeftCorner.x = transform.position.x - DistanceFromPlayerTo2Edge - DistanceFromPlayerTo1Edge;
            _lowerLeftCorner.y = transform.position.y - DistanceFromPlayerTo2Edge - DistanceFromPlayerTo1Edge;
            _upperRightCorner.x = transform.position.x + DistanceFromPlayerTo2Edge + DistanceFromPlayerTo1Edge;
            _upperRightCorner.y = transform.position.y + DistanceFromPlayerTo2Edge + DistanceFromPlayerTo1Edge;
        }
        public bool InWindow(Transform transform)
        {
            if (_lowerLeftCorner.x < transform.position.x  && transform.position.x < _upperRightCorner.x &&
                _lowerLeftCorner.y < transform.position.y && transform.position.y < _upperRightCorner.y)
            {
                return true;
            }
            return false;
        }
    }
    void MoveCamera(ref Window window, float _cameraHeight, Vector3 player, ref Vector3 camera,  ref Vector3 _oldPositionPlayer)
    {
        Vector3 deltaPosition = player - _oldPositionPlayer;
        if (!window.InWindow(transform))
        { 
            window._lowerLeftCorner.x += deltaPosition.x;
            window._lowerLeftCorner.y += deltaPosition.y;
            window._upperRightCorner.x += deltaPosition.x;
            window._upperRightCorner.y += deltaPosition.y;
            camera.x += deltaPosition.x;
            camera.y += deltaPosition.y;
        }
        camera.y = player.y + _cameraHeight;
        _oldPositionPlayer = player;
    }
    private void Start()
    {
        _cameraPosition = GetComponent<Transform>().position;
        _oldPositionPlayer = _playerPosition.position;
        _window = new Window(DistanceFromPlayerTo1Edge, DistanceFromPlayerTo2Edge,_playerPosition);
    }
    private void LateUpdate() 
    {
        MoveCamera(ref _window, _cameraHeight, _playerPosition.position, ref _cameraPosition,ref  _oldPositionPlayer);
    }
} 
