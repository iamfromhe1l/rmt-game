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
        
        private List<KeyValuePair<SphereCollider, DialogueScriptableObject>> _dialogueTriggers = new List<KeyValuePair<SphereCollider, DialogueScriptableObject>>();


        void Awake()
        {
            _camera = GetComponentInChildren<Camera>();
        }

        public void AddDialogue(SphereCollider trigger, DialogueScriptableObject dialogueData)
        {
            if (!_dialogueTriggers.Exists(pair => pair.Key == trigger))
                _dialogueTriggers.Add(new KeyValuePair<SphereCollider, DialogueScriptableObject>(trigger, dialogueData));
        }
        
        public void EndDialogue()
        {
            OnDialogueEnded?.Invoke();
        }

        private void OnControllerColliderHit(ControllerColliderHit other)
        {
            if (other.gameObject.CompareTag("HeroTag"))
            {
                foreach (var pair in _dialogueTriggers)
                {
                    if (other.collider == pair.Key)
                    {
                        Debug.Log("Started Dialogue");
                        //pair.Value.Initialize
                        OnDialogueStarted?.Invoke();
                        break;
                    }
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
