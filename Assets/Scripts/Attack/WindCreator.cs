using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Attack
{
    internal class WindCreator
    {
        public Wind CreateAttack(GameObject prefab, Vector3 startPoint, Vector3 rotation, Vector3 direction)
        {
            var gameObject = GameObject.Instantiate(prefab, startPoint, Quaternion.Euler(rotation));
            var attackComponent = gameObject.AddComponent<Wind>();
            attackComponent.Init(gameObject, direction);
            return attackComponent;
        }
    }
}
