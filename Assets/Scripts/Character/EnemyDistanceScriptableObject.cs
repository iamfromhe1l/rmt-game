using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Character
{
    [CreateAssetMenu(fileName = "EnemyDistanceScriptableObject", menuName = "ScriptableObjects/EnemyDistance")]
    public class EnemyDistanceScriptableObject : ScriptableObject
    {
        [SerializeField]
        private List<EnemyDistance> distances;
        public List<EnemyDistance> Distances => distances;
    }
}