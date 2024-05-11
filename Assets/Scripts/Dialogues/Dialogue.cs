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
        private Sounds _soundManager;
        private DialoguesManager _dialoguesManager;
        private string _participantTag;
        private Transform _childObject;
        private DialogueConfig _dialogueConfig;
        // public void Initialize(string dialogueId)
        // {
        //     // _textSpaceStoppingCoef = textSpaceCoef;
        //     // _textDisplayingSpeed = textDisplayingSpeed; TODO считывать из конфига
        // }

        void Awake()
        {
            _dialogueConfig = Resources.Load<DialogueConfig>("Dialogues/DialogueConfig");
            var dialogueScriptableObject = Resources.Load<DialogueScriptableObject>("Dialogues/Dialogs/" + gameObject.name);
            _dialogueLinesList = dialogueScriptableObject.DialogueLines;
            _dialoguesManager = FindObjectOfType<DialoguesManager>();
            _dialogueLines = new Queue<DialogueLine>(_dialogueLinesList);
            _soundManager = GetComponent<Sounds>();                          //TODO ВЫНЕСТИ саундс в менеджер и удалить из префаба
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
            SphereCollider triggerCollider = GameObject.FindWithTag(_participantTag).GetComponent<SphereCollider>();
            _dialoguesManager.AddDialogue(triggerCollider,this);
            
        }

        public void DisplayDialogue()
        {
            _childObject.gameObject.SetActive(true);
        }
        void Update()
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {

                if (_currentCoroutine != null)
                    _displayFullText = true;
                else
                {
                    if (_dialogueLines.Count > 0)
                    {
                        _dialogueLineText.text = "";
                        DialogueLine line = _dialogueLines.Dequeue();
                        _dialogueLineName.text= line.Participant.participantName;
                        // DisplayCamera(line.Participant.participantTag);
                        _currentCoroutine = StartCoroutine(DisplayText(line.text, line.Participant.voice));
                    }
                    else if (_currentCoroutine == null) // TODO проверить работает ли новая версия
                    {
                        _dialoguesManager.EndDialogue();
                        gameObject.SetActive(false);
                    }
                }
            }
        }
        // TODO не работает корутина
        IEnumerator DisplayText(string text, AudioClip voice)
        {
            for (int i = 0; i < text.Length; i++)
            {
                int isSpace = (text[i] == ' ' ? 1 : 0);
                float randSpace = isSpace * UnityEngine.Random.Range(1, 2) * _dialogueConfig._textDisplayingSpeed/_dialogueConfig._textSpaceStoppingCoef;
                _dialogueLineText.text += text[i];
                if (isSpace == 0) _soundManager.PlaySound(voice);
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
