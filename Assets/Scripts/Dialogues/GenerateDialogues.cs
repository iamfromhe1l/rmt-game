using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Dialogues
{
    public class GenerateDialog : ScriptableWizard
    {
        [SerializeField]
        private float _textDisplayingSpeed = 0.1f;
        [SerializeField]
        private List<DialogueLine> _dialogueLines;

        [MenuItem("GameObject/Generate Dialog")]
        private static void CreateWizard() => 
            DisplayWizard<GenerateDialog>("Generate Dialog");
        
        private void OnWizardCreate()
        {
            GameObject prefab = Resources.Load<GameObject>("[Interface]");
            GameObject instance = Instantiate(prefab);
            DialoguesManager dialoguesManager = instance.GetComponentInChildren<DialoguesManager>();
            GameObject interfaceObject = GameObject.Find("[Interface]");
            if (!interfaceObject)
                instance.name = prefab.name;
            else
            {
                Transform canvasTransform = interfaceObject.transform.Find("Canvas");
                dialoguesManager.transform.SetParent(canvasTransform);
                DestroyImmediate(instance);
            }
            dialoguesManager.Initialize(_textDisplayingSpeed, _dialogueLines);
        }
    }
}