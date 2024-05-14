using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Character
{
    [System.Serializable]
    public class EnemyDistance
    {
        [SerializeField]
        private EnemyTypes enemyType;
        public EnemyTypes EnemyType => enemyType;
        [SerializeField]
        private float enemyDistance;
        public float Distance => enemyDistance;

    }
}