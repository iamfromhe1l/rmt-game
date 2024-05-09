using Assets.Scripts;
using Assets.Scripts.River;
using System.Runtime.CompilerServices;
using UnityEngine;

internal class FireballCreator
{
    public Fireball CreateAttack(GameObject prefab, Vector3 startPoint, Vector3 rotation, Vector3 direction)
    {
        var gameObject = GameObject.Instantiate(prefab, startPoint, Quaternion.Euler(rotation));
        var attackComponent = gameObject.GetComponent<Fireball>();
        attackComponent.Init(startPoint ,gameObject, direction);
        return attackComponent;
    }
}
