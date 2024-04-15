using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Dialogues
{
    public class DialoguesManager : MonoBehaviour
    {
        [SerializeField]
        private float _textDisplayingSpeed = 0.1f;
        [SerializeField]
        private int _textSpaceStoppingCoef = 5;
        [SerializeField]
        private List<DialogueLine> _dialogueLinesList;
        private Queue<DialogueLine> _dialogueLines;
        private TMP_Text _dialogueLineText;
        private TMP_Text _dialogueLineName;
        private Coroutine _currentCoroutine;
        private bool _displayFullText;
        private DialogueParticipantsScriptableObject _dialogueParticipants;
        private Sounds _soundManager;
    
        [SerializeField]
        private Camera camera;

        [SerializeField] 
        private float distanceToCamera = 2f;
        [SerializeField] 
        private float heightToCamera = 0.5f;
        
        
        public void Initialize(float textDisplayingSpeed, List<DialogueLine> dialogueLines)
        {
            _textDisplayingSpeed = textDisplayingSpeed;
            _dialogueLinesList = dialogueLines;
        }

        void Awake()
        {
            _dialogueLines = new Queue<DialogueLine>(_dialogueLinesList);
            _soundManager = GetComponent<Sounds>();
            Transform imageChild = transform.Find("Image");
            _dialogueLineText = imageChild.Find("Text").GetComponent<TMP_Text>();
            _dialogueLineName = imageChild.Find("Name").GetComponent<TMP_Text>();
            _dialogueParticipants = Resources.Load<DialogueParticipantsScriptableObject>("DialogueParticipants");
            foreach (var line in _dialogueLinesList)
            {
                var participant = _dialogueParticipants.Participants.Find(p => p.participantTag == line.participantTag);
                line.SetDialogueParticipant(participant);
            }
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {

                if (_currentCoroutine != null)
                    _displayFullText = true;
                else
                    if (_dialogueLines.Count > 0)
                    {
                        _dialogueLineText.text = "";
                        DialogueLine line = _dialogueLines.Dequeue();
                        _dialogueLineName.text= line.Participant.participantName;
                        CreateCamera(line.Participant.participantTag);
                        _currentCoroutine = StartCoroutine(DisplayText(line.text, line.Participant.voice));
                    }
                
                if (_currentCoroutine == null) // Dont work, fix вложенность if
                {
                    //disable dialogues here
                    // this.enabled = false;
                }
            }
        }
        IEnumerator DisplayText(string text, AudioClip voice)
        {
            for (int i = 0; i < text.Length; i++)
            {
                int isSpace = (text[i] == ' ' ? 1 : 0);
                float randSpace = isSpace * UnityEngine.Random.Range(1, 2) * _textDisplayingSpeed/_textSpaceStoppingCoef;
                _dialogueLineText.text += text[i];
                if (isSpace == 0) _soundManager.PlaySound(voice);
                if (_displayFullText)
                {
                    _dialogueLineText.text = text;
                    break;
                }
                yield return new WaitForSeconds(_textDisplayingSpeed + randSpace);
                if (_displayFullText)
                {
                    _dialogueLineText.text = text;
                    break;
                }
            }
            _currentCoroutine = null;
            _displayFullText = false;
        }

        void CreateCamera(string participantTag)
        {
            // Use FindWithTag in Manager once, not here
            GameObject obj = GameObject.FindWithTag(participantTag);
            Vector3 offset = obj.transform.forward;
            Vector3 cameraPosition = obj.transform.position + offset * distanceToCamera;
            camera.transform.position = cameraPosition;
            camera.transform.LookAt(obj.transform);
            camera.transform.position += new Vector3(0, heightToCamera, 0);
        }
    }
}
