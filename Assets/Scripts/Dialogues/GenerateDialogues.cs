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
        private GameObject participant;

        [MenuItem("GameObject/Generate Dialog")]
        private static void CreateWizard() => 
            DisplayWizard<GenerateDialog>("Generate Dialog");

        private void OnWizardUpdate()
        {
            Debug.Log("Fasfasf");
        }

        private void OnWizardCreate()
        {
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
            dialogue.Initialize(_textDisplayingSpeed, _dialogueLines);
        }
    }
}