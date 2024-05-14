using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Attack
{
    internal class SwordCreator
    {
        public Sword CreateAttack(GameObject prefab, Vector3 startPoint, Vector3 rotation)
        {
            var gameObject = GameObject.Instantiate(prefab, startPoint, Quaternion.Euler(rotation));
            var attackComponent = gameObject.AddComponent<Sword>();
            attackComponent.Init(gameObject);
            return attackComponent;
        }
    }
}
