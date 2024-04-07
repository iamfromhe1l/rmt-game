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
        private List<DialogueLine> _dialogueLinesList;
        private Queue<DialogueLine> _dialogueLines;
        [SerializeField]
        private TMP_Text _dialogueLineText;
        private TMP_Text _dialogueLineName;
        private Coroutine _currentCoroutine;
        private bool _displayFullText;

        
        public void Initialize(float textDisplayingSpeed, List<DialogueLine> dialogueLines)
        {
            _textDisplayingSpeed = textDisplayingSpeed;
            _dialogueLinesList = dialogueLines;
        }
        
        void Awake()
        {
            _dialogueLines = new Queue<DialogueLine>(_dialogueLinesList);
            // get TMP_Text components
        }
        void Update()
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                if (_dialogueLines.Count > 0)
                {
                    if (_currentCoroutine != null)
                        _displayFullText = true;
                    else
                    {
                        _dialogueLineText.text = "";
                        _currentCoroutine = StartCoroutine(DisplayText(_dialogueLines.Dequeue().text));
                    }
                }
                else // disable dialogue
                if (_currentCoroutine == null)
                    _dialogueLineText.enabled = false;
            }
        }
        IEnumerator DisplayText(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                float randSpace = (text[i] == ' ' ? 1 : 0) * UnityEngine.Random.Range(1, 2) * _textDisplayingSpeed/5;
                _dialogueLineText.text += text[i];
                if (_displayFullText)
                {
                    _dialogueLineText.text = text;
                    break;
                }
                yield return new WaitForSeconds(_textDisplayingSpeed + randSpace);
            }
            _currentCoroutine = null;
            _displayFullText = false;
        }
    }
}
