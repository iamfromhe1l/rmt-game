using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Character
{
    [System.Serializable]
    public class EnemyDistance
    {
        [SerializeField]
        private EnemyTypes enemytype;
        public EnemyTypes Enemytype => enemytype;
        [SerializeField]
        private float enemydistance;
        public float Enemydistance => enemydistance;

    }
}