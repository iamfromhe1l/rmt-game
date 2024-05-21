using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Dialogues
{
    public class Dialogue : MonoBehaviour
    {
        private List<DialogueLine> _dialogueLinesList;
        private Queue<DialogueLine> _dialogueLines;
        private TMP_Text _dialogueLineText;
        private TMP_Text _dialogueLineName;
        private Coroutine _currentCoroutine;
        private bool _displayFullText;
        private DialogueParticipantsScriptableObject _dialogueParticipants;
        private DialoguesManager _dialoguesManager;
        private string _participantTag;
        private Transform _childObject;
        private DialogueConfig _dialogueConfig;
        private bool _isRepeatable;
        public bool IsRepeatable => _isRepeatable;
        private bool _isPassed = false;
        public bool IsPassed => _isPassed;
        private Canvas _starterCanvas;
        private bool _isClicked = true;


        void Awake()
        {
            _dialogueConfig = Resources.Load<DialogueConfig>("Dialogues/DialogueConfig");
            DialogueScriptableObject dialogue = Resources.Load<DialogueScriptableObject>("Dialogues/Dialogs/" + gameObject.name);
            _dialogueLinesList = dialogue.DialogueLines;
            _isRepeatable = dialogue.IsRepeatable;
            _dialoguesManager = FindObjectOfType<DialoguesManager>();
            _childObject = transform.GetChild(0);
            Transform imageChild = _childObject.Find("Image");
            _dialogueLineText = imageChild.Find("Text").GetComponent<TMP_Text>();
            _dialogueLineName = imageChild.Find("Name").GetComponent<TMP_Text>();
            _dialogueParticipants = Resources.Load<DialogueParticipantsScriptableObject>("Dialogues/DialogueParticipants");
            foreach (var line in _dialogueLinesList)
            {
                if (line.participantTag != "HeroTag")
                    _participantTag = line.participantTag;
                var participant = _dialogueParticipants.Participants.Find(p => p.participantTag == line.participantTag);
                line.SetDialogueParticipant(participant);
            }
            Transform participantTranform = GameObject.FindWithTag(_participantTag).transform;
            SphereCollider triggerCollider = participantTranform.Find("DialogueTriggerObject").GetComponent<SphereCollider>();
            DialogueTrigger dialogueTrigger = participantTranform.Find("DialogueTriggerObject").GetComponent<DialogueTrigger>();
            dialogueTrigger.onTriggerExitAction += OnTriggerExit;
            _dialoguesManager.AddDialogue(triggerCollider,this);
            _starterCanvas = participantTranform.Find("HintCanvas").Find("Canvas").GetComponent<Canvas>();
        }

        public void OnTriggerExit()
        {
            _starterCanvas.gameObject.SetActive(false);
        }
        public void DisplayDialogue()
        {
            _dialogueLines = new Queue<DialogueLine>(_dialogueLinesList);
            if (_isPassed && _isRepeatable)
                _starterCanvas.gameObject.SetActive(true);
            else
            {
                _childObject.gameObject.SetActive(true);
                StartNextDialogueLine();
            }
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && (_isPassed && _isRepeatable))
            {
                _isClicked = true;
                _childObject.gameObject.SetActive(true);
                _starterCanvas.gameObject.SetActive(false);
                StartNextDialogueLine();
            }
                
            if (_isClicked && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)))
                if (_childObject.gameObject.activeSelf)
                    StartNextDialogueLine();
        }

        private void StartNextDialogueLine()
        {
            if (_currentCoroutine != null)
                _displayFullText = true;
            else
            {
                if (_dialogueLines.Count > 0)
                {
                    // _dialogueLineText.text = "";
                    // DialogueLine line = _dialogueLines.Dequeue();
                    // _dialogueLineName.text = line.Participant.participantName;
                    // _dialoguesManager.DisplayCamera(line.Participant.participantTag);
                    // _currentCoroutine = StartCoroutine(DisplayText(line.text, line.Participant.voice));
                    
                    _dialogueLineText.text = "";
                    DialogueLine line = _dialogueLines.Peek();
                    if (string.IsNullOrWhiteSpace(line.text))
                    {
                        _dialogueLines.Dequeue();
                        StartNextDialogueLine();
                    }
                    else
                    {
                        line = _dialogueLines.Dequeue();
                        _dialogueLineName.text = line.Participant.participantName;
                        _dialoguesManager.DisplayCamera(line.Participant.participantTag);
                        _currentCoroutine = StartCoroutine(DisplayText(line.text, line.Participant.voice));
                    }
                }
                else if (_currentCoroutine == null)
                {
                    _isPassed = true;
                    _isClicked = false;
                    _dialoguesManager.EndDialogue();
                    _childObject.gameObject.SetActive(false);
                }
            }
        }

        IEnumerator DisplayText(string text, AudioClip voice)
        {
            for (int i = 0; i < text.Length; i++)
            {
                int isSpace = (text[i] == ' ' ? 1 : 0);
                float randSpace = isSpace * UnityEngine.Random.Range(1, 2) * _dialogueConfig._textDisplayingSpeed/_dialogueConfig._textSpaceStoppingCoef;
                _dialogueLineText.text += text[i];
                if (isSpace == 0) _dialoguesManager.PlaySound(voice);
                if (_displayFullText)
                {
                    _dialogueLineText.text = text;
                    break;
                }
                yield return new WaitForSeconds(_dialogueConfig._textDisplayingSpeed + randSpace);
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
