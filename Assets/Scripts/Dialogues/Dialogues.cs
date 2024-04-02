using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogues : MonoBehaviour
{
    [System.Serializable]
    public class DialogueLine
    {
        public GameObject participant;
        public string text;
    }
    
    [SerializeField]
    private List<DialogueLine> _dialogueLines;

    [SerializeField]
    private TMP_Text _currentLine;

    private int _dialoguesCount;

    private int curr = 0;

    void Awake()
    {
        _dialoguesCount = _dialogueLines.Count;
    }
    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (_dialoguesCount > 0)
            {
                _currentLine.text = _dialogueLines[curr].text;
                _dialoguesCount--;
                curr++;
            }
            else
            {
                Debug.Log("Dialogues End");
            }
            Debug.Log(_currentLine.text);
        }
    }
}
