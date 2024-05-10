using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/Dialogue", order = 3)]
    public class DialogueScriptableObject : ScriptableObject
    {
        [SerializeField]
        private List<DialogueLine> _dialogueLines;
        [SerializeField]
        private bool _isRepeatable;
        
        public List<DialogueLine> DialogueLines => _dialogueLines;
    }
}
