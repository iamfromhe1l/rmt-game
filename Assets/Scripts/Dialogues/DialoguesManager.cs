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
        
        
        public void Initialize(float textDisplayingSpeed, List<DialogueLine> dialogueLines)
        {
            _textDisplayingSpeed = textDisplayingSpeed;
            _dialogueLinesList = dialogueLines;
        }

        void Awake()
        {
            _dialogueLines = new Queue<DialogueLine>(_dialogueLinesList);
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
                        _currentCoroutine = StartCoroutine(DisplayText(line.text));
                    }
                
                if (_currentCoroutine == null)
                {
                    //disable dialogues here
                    // this.enabled = false;
                }
            }
        }
        IEnumerator DisplayText(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                float randSpace = (text[i] == ' ' ? 1 : 0) * UnityEngine.Random.Range(1, 2) * _textDisplayingSpeed/_textSpaceStoppingCoef;
                _dialogueLineText.text += text[i];
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
