using System;
using UnityEngine;

namespace Dialogues
{
    public class CanvasRotater : MonoBehaviour
    {
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
            transform.Find("Canvas").GetComponent<Canvas>().worldCamera = _camera;
        }

        void Update()
        {
            transform.LookAt(_camera!.transform);
        }
    }
}