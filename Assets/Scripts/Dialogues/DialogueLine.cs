
using UnityEngine;

namespace Dialogues
{
    [System.Serializable]
    public class DialogueLine
    {
        public string participantTag;
        public string text;
        private DialogueParticipant _participant;
        public DialogueParticipant Participant => _participant;
        
        public void SetDialogueParticipant(DialogueParticipant participant)
        {
            _participant = new DialogueParticipant(participant.participantName, participant.participantTag,
                participant.voice);
        }
    }
}