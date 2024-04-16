using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Assets.Scripts.Character
{
    public class Enemy : Character
    {
        PatroleTypes patroleType;
        float minDistance;

        void Initialize(EnemyTypes type)
        {
            var allDistanceInfos = Resources.Load<EnemyDistanceScriptableObject>("EnemyDistanceScriptableObject");
            minDistance = allDistanceInfos.Distances.Where(p => p.EnemyType == type).First().Distance;
        }
        override protected void Death()
        {

        }
        protected void GodAction()
        {
            //Помянем
        }

        
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
