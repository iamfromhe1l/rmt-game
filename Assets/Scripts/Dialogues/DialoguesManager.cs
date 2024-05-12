using System;
using System.Collections.Generic;
using Sounds;
using UnityEngine;

namespace Dialogues
{
    public delegate void DialogueHandler();
    [RequireComponent(typeof(SoundsController))]
    public class DialoguesManager : MonoBehaviour
    {
        public event DialogueHandler OnDialogueStarted;
        public event DialogueHandler OnDialogueEnded;
        private Camera _camera;
        private SoundsController _soundManager;
        private readonly List<KeyValuePair<SphereCollider, Dialogue>> _dialogueTriggers = new();
        private Dictionary<string, GameObject> _taggedObjects = new();
        private DialogueConfig _dialogueConfig;
        private Queue<Dialogue> _dialogueQueue;

        void Awake()
        {
            _dialogueConfig = Resources.Load<DialogueConfig>("Dialogues/DialogueConfig");
            _soundManager = GetComponent<SoundsController>();
            GameObject cameraPrefab = Resources.Load<GameObject>("ParticipantCamera");
            GameObject cameraObject = Instantiate(cameraPrefab, transform, true);
            _camera = cameraObject.GetComponent<Camera>();
        }

        public void AddDialogue(SphereCollider trigger, Dialogue dialogueData)
        {
            if (!_dialogueTriggers.Exists(pair => pair.Key == trigger && pair.Value == dialogueData))
                _dialogueTriggers.Add(new KeyValuePair<SphereCollider, Dialogue>(trigger, dialogueData));
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        public void EndDialogue()
        {
            if (_dialogueQueue.Count > 0)
            {
                _dialogueQueue.Dequeue().DisplayDialogue();
                return;
            }
            OnDialogueEnded?.Invoke();
        }
        
        public void StartDialogue(DialogueTrigger other)
        {
            _dialogueQueue = new Queue<Dialogue>();
            List<Dialogue> dialogues = new List<Dialogue>();
            foreach (var pair in _dialogueTriggers)
                if (other.triggerCollider == pair.Key && (pair.Value.IsRepeatable || !pair.Value.IsPassed))
                    dialogues.Add(pair.Value);
            dialogues.Sort((dialogue1, dialogue2) => String.CompareOrdinal(dialogue1.gameObject.name, dialogue2.gameObject.name));
            foreach (var dialogue in dialogues)
                _dialogueQueue.Enqueue(dialogue);
            if (_dialogueQueue.Count > 0)
            {
                _dialogueQueue.Dequeue().DisplayDialogue();
                OnDialogueStarted?.Invoke();
            }
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        public void DisplayCamera(string participantTag)
        {
            if (!_taggedObjects.TryGetValue(participantTag, out GameObject obj))
            {
                obj = GameObject.FindWithTag(participantTag);
                _taggedObjects[participantTag] = obj;
            }
            Vector3 offset = obj.transform.forward;
            Vector3 cameraPosition = obj.transform.position + offset * _dialogueConfig.distanceToCamera;
            _camera.transform.position = cameraPosition;
            _camera.transform.LookAt(obj.transform);
            _camera.transform.position += new Vector3(0, _dialogueConfig.heightToCamera, 0);
        }
        
        public void PlaySound(AudioClip voice)
        {
            _soundManager.PlaySound(voice);
        }

    }

    
}
