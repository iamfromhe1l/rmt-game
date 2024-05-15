using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace Dialogues
{
    public class GenerateDialog : ScriptableWizard
    {
        [SerializeField]
        private List<DialogueScriptableObject> dialogues;
        private GameObject _dialoguesManagerObject;
        private float _colliderRadius;
        private GameObject _prefab;
        private bool _isOnesCreated = false;

        [MenuItem("GameObject/Generate Dialog")]
        private static void CreateWizard() => DisplayWizard<GenerateDialog>("Generate Dialog");
        
        private void OnWizardCreate()
        {
            if (!FindObjectOfType<DialoguesManager>())
                new GameObject("DialoguesManager").AddComponent<DialoguesManager>();
            _colliderRadius = Resources.Load<DialogueConfig>("Dialogues/DialogueConfig").colliderRadius;
            _prefab = Resources.Load<GameObject>("[Interface]");
            foreach (var dialogue in dialogues)
                CreateOneDialogue(dialogue);
        }

        private void CreateOneDialogue(DialogueScriptableObject oneDialogue)
        {
            GameObject instance = Instantiate(_prefab);
            Dialogue dialogue = instance.GetComponentInChildren<Dialogue>();
            GameObject interfaceObject = GameObject.Find("[Interface]");
            if (!interfaceObject)
                instance.name = _prefab.name;
            else
            {
                Transform canvasTransform = interfaceObject.transform.Find("Canvas");
                dialogue.transform.SetParent(canvasTransform);
                DestroyImmediate(instance);
            }
            dialogue.gameObject.name = oneDialogue.name; 
            foreach (var par in oneDialogue.DialogueLines)
            {
                if (par.participantTag != "HeroTag" && !_isOnesCreated)
                {
                    GameObject participantObject = GameObject.FindWithTag(par.participantTag);
                    GameObject obj = participantObject.transform.Find("DialogueTriggerObject")?.gameObject;
                    if (obj == null)
                    {
                        obj = new GameObject("DialogueTriggerObject");
                        obj.transform.parent = participantObject.transform;
                        obj.transform.position = participantObject.transform.position;
                    }
                    if (obj.GetComponent<SphereCollider>() == null)
                    {
                        var sphereCollider = obj.AddComponent<SphereCollider>();
                        sphereCollider.radius = _colliderRadius;
                        sphereCollider.isTrigger = true;
                    }
                    if (obj.GetComponent<DialogueTrigger>() == null)
                        obj.AddComponent<DialogueTrigger>();
                    
                    GameObject hintPrefab = Resources.Load<GameObject>("HintCanvas");
                    Canvas existingCanvas = participantObject.GetComponentInChildren<Canvas>();
                    if (existingCanvas == null)
                    {
                        var dialogueHint = Instantiate(hintPrefab, participantObject.transform, false);
                        dialogueHint.transform.SetParent(participantObject.transform, false);
                        var pos = participantObject.transform.position;
                        dialogueHint.transform.position = new Vector3(pos.x, pos.y + 2, pos.z);
                        dialogueHint.transform.localRotation = Quaternion.identity;
                        dialogueHint.name = "HintCanvas";
                    }
                    _isOnesCreated = true;
                    break;
                }
            }
            dialogue.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}