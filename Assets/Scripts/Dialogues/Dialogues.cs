using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogues : MonoBehaviour
{
    [SerializeField]
    private float _textDisplayingSpeed = 0.1f;
    [SerializeField]
    private List<DialogueLine> _dialogueLinesList;
    private Queue<DialogueLine> _dialogueLines;
    [SerializeField]
    private TMP_Text _canvasText;

    private Coroutine _currentCoroutine;
    private bool _displayFullText = false;

    void Awake()
    {
        _dialogueLines = new Queue<DialogueLine>(_dialogueLinesList);
    }
    void Start()
    {
        
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
                    _canvasText.text = "";
                    _currentCoroutine = StartCoroutine(DisplayText(_dialogueLines.Dequeue().text));
                }

            }
            else // disable dialogue
                if (_currentCoroutine == null)
                    _canvasText.enabled = false;
        }
    }
    IEnumerator DisplayText(string text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            _canvasText.text += text[i];
            if (_displayFullText)
            {
                _canvasText.text = text;
                break;
            }
            yield return new WaitForSeconds(_textDisplayingSpeed);
        }
        _currentCoroutine = null;
        _displayFullText = false;
    }
}
