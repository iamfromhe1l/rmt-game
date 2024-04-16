using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    public delegate void DialogueHandler();
    public class DialoguesManager : MonoBehaviour
    {
        public event DialogueHandler OnDialogueStarted;
        public event DialogueHandler OnDialogueEnded;
        private Camera _camera;
        
        private List<KeyValuePair<Collider, Dialogue>> _dialogueTriggers = new List<KeyValuePair<Collider, Dialogue>>();


        void Awake()
        {
            _camera = GetComponentInChildren<Camera>();
        }

        public void AddDialogue(Collider trigger, Dialogue dialogueData)
        {
            if (!_dialogueTriggers.Exists(pair => pair.Key == trigger))
                _dialogueTriggers.Add(new KeyValuePair<Collider, Dialogue>(trigger, dialogueData));
        }

        private void OnTriggerEnter(Collider other)
        {
            foreach (var pair in _dialogueTriggers)
            {
                if (other == pair.Key)
                {
                    OnDialogueStarted?.Invoke();
                    break;
                }
            }
        }
        
        public void DisplayCamera(string participantTag)
        {
            // Use FindWithTag in Manager once, not here
            GameObject obj = GameObject.FindWithTag(participantTag);
            Vector3 offset = obj.transform.forward;
            // Vector3 cameraPosition = obj.transform.position + offset * distanceToCamera;
            // _camera.transform.position = cameraPosition;
            _camera.transform.LookAt(obj.transform);
            // _camera.transform.position += new Vector3(0, heightToCamera, 0);
        }

    }

    
}
