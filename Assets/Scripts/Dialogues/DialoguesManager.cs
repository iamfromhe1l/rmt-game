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
        
        private readonly List<KeyValuePair<SphereCollider, Dialogue>> _dialogueTriggers = new List<KeyValuePair<SphereCollider, Dialogue>>();


        void Awake()
        {
            _camera = GetComponentInChildren<Camera>();
        }

        public void AddDialogue(SphereCollider trigger, Dialogue dialogueData)
        {
            Debug.Log("Enter to Add Dialogue");
            if (!_dialogueTriggers.Exists(pair => pair.Key == trigger))
                _dialogueTriggers.Add(new KeyValuePair<SphereCollider, Dialogue>(trigger, dialogueData));
        }
        
        public void EndDialogue()
        {
            OnDialogueEnded?.Invoke();
        }

        public void StartDialogue(DialogueTrigger other)
        {
            Debug.Log(_dialogueTriggers.Count);
            Debug.Log(_dialogueTriggers[0]);
            Debug.Log(_dialogueTriggers[0].Key);
            Debug.Log(other.triggerCollider);
            foreach (var pair in _dialogueTriggers)
            {
                if (other.triggerCollider == pair.Key)
                {
                    Debug.Log("Started Dialogue");
                    pair.Value.DisplayDialogue();
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
