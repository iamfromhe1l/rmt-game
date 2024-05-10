using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Dialogues
{
    public class GenerateDialog : ScriptableWizard
    {
        [SerializeField]
        private List<DialogueScriptableObject> _dialogues;

        private GameObject _dialoguesManagerObject;

        [MenuItem("GameObject/Generate Dialog")]
        private static void CreateWizard() => DisplayWizard<GenerateDialog>("Generate Dialog");
        
        private void OnWizardCreate()
        {
            DialogueConfig config = Resources.Load<DialogueConfig>("Dialogues/DialogueConfig");
            
            GameObject prefab = Resources.Load<GameObject>("[Interface]");
            GameObject instance = Instantiate(prefab);
            Dialogue dialogue = instance.GetComponentInChildren<Dialogue>();
            GameObject interfaceObject = GameObject.Find("[Interface]");
            if (!interfaceObject)
                instance.name = prefab.name;
            else
            {
                Transform canvasTransform = interfaceObject.transform.Find("Canvas");
                dialogue.transform.SetParent(canvasTransform);
                DestroyImmediate(instance);
            }
            DialoguesManager dialoguesManager = FindObjectOfType<DialoguesManager>();
            if (dialoguesManager == null)
            {
                _dialoguesManagerObject = new GameObject("DialoguesManager");
                dialoguesManager = _dialoguesManagerObject.AddComponent<DialoguesManager>();
            }

            SphereCollider sphereCollider = null;
            foreach (var par in _dialogues[0].DialogueLines)
            {
                if (par.participantTag != "HeroTag")
                {
                    GameObject participantObject = GameObject.FindWithTag(par.participantTag);
                    sphereCollider = participantObject.AddComponent<SphereCollider>();
                    sphereCollider.radius = config.colliderRadius;
                    sphereCollider.isTrigger = true;
                    break;
                }
            }
            dialoguesManager.AddDialogue(sphereCollider,_dialogues[0]);
            // dialogue.Initialize(_textDisplayingSpeed, _dialogueLines); TODO понять нужно ли
        }
    }
}