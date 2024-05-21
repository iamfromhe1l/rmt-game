using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

internal class FireballCreator
{
    private float _deltaDistance = 1f;
    private Vector3 _currentPos;
    private float _deltaToPlayer = 0.3f;
    private List<Fireball> _fireballList = new();
    public List<Fireball> CreateAttack(GameObject prefab, Vector3 startPoint, Vector3 rotation, 
        Vector3 direction, int fireballCount)
    {
        for (int i = -fireballCount/2; i < fireballCount/2; i++)
        {
            if (fireballCount % 2 == 0)
                _currentPos = startPoint + (Quaternion.Euler(0, 90, 0) * direction * _deltaDistance * i)
                    + Quaternion.Euler(0, 90, 0) * direction * _deltaToPlayer;
            else
                _currentPos = startPoint + (Quaternion.Euler(0, 90, 0) * direction * _deltaDistance * i);
            var gameObject = GameObject.Instantiate(prefab, _currentPos, Quaternion.Euler(rotation));
            var attackComponent = gameObject.AddComponent<Fireball>();
            attackComponent.Init(gameObject, direction);
            _fireballList.Add(attackComponent);
        }
        return _fireballList;
    }
}
