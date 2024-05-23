using UnityEngine;

namespace Assets.Scripts.Attack
{
    internal class AxeCreator
    {
        public Axe CreateAttack(GameObject prefab, Vector3 startPoint, Vector3 rotation,
            float radius, Vector3 center, float angle, string tagEnemy)
        {
            var gameObject = GameObject.Instantiate(prefab, startPoint, Quaternion.Euler(rotation));
            var attackComponent = gameObject.AddComponent<Axe>();
            attackComponent.Init(gameObject, radius, center, angle, tagEnemy);
            return attackComponent;
        }
    }
}

