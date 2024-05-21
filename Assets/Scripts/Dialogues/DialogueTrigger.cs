using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Dialogues
{
    public class DialogueTrigger : MonoBehaviour
    {
        public SphereCollider triggerCollider;
        private DialoguesManager _dialoguesManager;
        public Action onTriggerExitAction;

        private void Awake()
        {
            triggerCollider = GetComponent<SphereCollider>();
            _dialoguesManager= FindObjectOfType<DialoguesManager>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("HeroTag"))
                _dialoguesManager.StartDialogue(this);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("HeroTag"))
                onTriggerExitAction.Invoke();
        }
    }
}