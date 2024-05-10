using UnityEngine;

namespace Dialogues
{
    [CreateAssetMenu(fileName = "DialogueConfig", menuName = "ScriptableObjects/DialogueConfig", order = 4)]
    public class DialogueConfig : ScriptableObject
    {
        [SerializeField][Header("Время остановки между символами")]
        public float _textDisplayingSpeed = 0.1f;
        [SerializeField][Header("Коэф. остановки между словами")]
        public int _textSpaceStoppingCoef = 5;
        [SerializeField][Header("Расстояние между камерой и персонажем")]
        public float distanceToCamera = 2f;
        [SerializeField][Header("Высота камеры")]
        public float heightToCamera = 0.5f;
        [SerializeField][Header("Радиус коллайдера")]
        public float colliderRadius = 2f;
    }
}
