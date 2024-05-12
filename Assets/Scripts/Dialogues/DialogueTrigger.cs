using UnityEngine;

namespace Dialogues
{
    public class DialogueTrigger : MonoBehaviour
    {
        public SphereCollider triggerCollider;
        private DialoguesManager _dialoguesManager;

        private void Awake()
        {
            triggerCollider = GetComponent<SphereCollider>();
            _dialoguesManager= FindObjectOfType<DialoguesManager>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("HeroTag"))
            {
                _dialoguesManager.StartDialogue(this);
            }
        }
    }
}