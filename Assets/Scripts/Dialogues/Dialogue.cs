using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Dialogues
{
    public class Dialogue : MonoBehaviour
    {
        private float _textDisplayingSpeed;
        private int _textSpaceStoppingCoef;
        private List<DialogueLine> _dialogueLinesList;
        private Queue<DialogueLine> _dialogueLines;
        private TMP_Text _dialogueLineText;
        private TMP_Text _dialogueLineName;
        private Coroutine _currentCoroutine;
        private bool _displayFullText;
        private DialogueParticipantsScriptableObject _dialogueParticipants;
        private Sounds _soundManager;
        private DialoguesManager _dialoguesManager;

        
        
        public void Initialize(int textSpaceCoef, float textDisplayingSpeed, List<DialogueLine> dialogueLines)
        {
            _textSpaceStoppingCoef = textSpaceCoef;
            _textDisplayingSpeed = textDisplayingSpeed;
            _dialogueLinesList = dialogueLines;
        }

        void Awake()
        {
            _dialoguesManager= FindObjectOfType<DialoguesManager>();
            _dialoguesManager.OnDialogueStarted += DisplayDialogue;
            _dialogueLines = new Queue<DialogueLine>(_dialogueLinesList);
            _soundManager = GetComponent<Sounds>();                             //TODO ВЫНЕСТИ
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
        
        void OnEnable()
        {
            _dialoguesManager.OnDialogueStarted += DisplayDialogue;
        }

        private void OnDisable()
        {
            _dialoguesManager.OnDialogueStarted -= DisplayDialogue;
        }
        
        void DisplayDialogue()
        {
            gameObject.SetActive(true);
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
                        // DisplayCamera(line.Participant.participantTag);
                        _currentCoroutine = StartCoroutine(DisplayText(line.text, line.Participant.voice));
                    }
                
                if (_currentCoroutine == null) // Dont work, fix вложенность if
                {
                    // gameObject.SetActive(false);
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


    }
}
