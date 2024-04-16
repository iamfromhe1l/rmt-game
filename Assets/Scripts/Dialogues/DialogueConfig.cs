using UnityEngine;

namespace Dialogues
{
    [CreateAssetMenu(fileName = "DialogueConfig", menuName = "ScriptableObjects/DialogueConfig", order = 4)]
    public class DialogueConfig : ScriptableObject
    {
        [SerializeField][Header("Время остановки между символами")]
        private float _textDisplayingSpeed = 0.1f;
        [SerializeField][Header("Коэф. остановки между словами")]
        private int _textSpaceStoppingCoef = 5;
        [SerializeField][Header("Расстояние между камерой и персонажем")]
        private float distanceToCamera = 2f;
        [SerializeField][Header("Высота камеры")]
        private float heightToCamera = 0.5f;
    }
}
