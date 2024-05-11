using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// TODO у меня может быть 2 диалога в одном

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
            if (!FindObjectOfType<DialoguesManager>()) // TODO проверить работает ли
                new GameObject("DialoguesManager").AddComponent<DialoguesManager>();
            _colliderRadius = Resources.Load<DialogueConfig>("Dialogues/DialogueConfig").colliderRadius;
            _prefab = Resources.Load<GameObject>("[Interface]");
            
            
            // TODO где используется ис репитабл?
            
            
            foreach (var dialogue in dialogues)
            {
                CreateOneDialogue(dialogue);
            }
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
                    var sphereCollider = participantObject.AddComponent<SphereCollider>();
                    sphereCollider.radius = _colliderRadius;
                    sphereCollider.isTrigger = true;
                    participantObject.AddComponent<DialogueTrigger>(); // TODO два триггера добавялется
                    _isOnesCreated = true;
                    break;
                }
            }
            dialogue.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}