using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    [CreateAssetMenu(fileName = "DialogueParticipants", menuName = "ScriptableObjects/DialogueParticipants", order = 1)]
    public class DialogueParticipantsScriptableObject : ScriptableObject
    {
        [SerializeField]
        private List<DialogueParticipant> participants;
        public List<DialogueParticipant> Participants => participants;
    }
}