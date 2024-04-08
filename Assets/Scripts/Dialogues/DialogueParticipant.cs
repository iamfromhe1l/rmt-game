using UnityEngine;

namespace Dialogues
{
    [System.Serializable]
    public class DialogueParticipant
    {
        public string participantName;
        public string participantTag;
        public AudioClip voice;

        public DialogueParticipant(string participantName, string participantTag, AudioClip voice)
        {
            this.participantName = participantName;
            this.participantTag = participantTag;
            this.voice = voice;
        }
    }
}
