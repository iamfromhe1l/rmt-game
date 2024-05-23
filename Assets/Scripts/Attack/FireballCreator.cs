using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

internal class FireballCreator
{
    private float _deltaDistance = 1f;
    private Vector3 _currentPos;
    private float _deltaToPlayer = 0.3f;
    private List<Fireball> _fireballList = new List<Fireball>();

    public List<Fireball> CreateAttack(GameObject prefab, Vector3 startPoint, Vector3 rotation,
        Vector3 direction, int fireballCount)
    {
        if (fireballCount == 1)
        {
            var gameObject = GameObject.Instantiate(prefab, startPoint, Quaternion.Euler(rotation));
            var attackComponent = gameObject.AddComponent<Fireball>();
            attackComponent.Init(gameObject, direction);
            _fireballList.Add(attackComponent);
        }
        else
        {
            for (int i = 0; i < fireballCount; i++)
            {
                float offset = (_deltaDistance * i) - (_deltaDistance * (fireballCount - 1) / 2);

                if (fireballCount % 2 == 0)
                {
                    _currentPos = startPoint + (Quaternion.Euler(0, 90, 0) * direction * offset)
                                             + Quaternion.Euler(0, 90, 0) * direction * _deltaToPlayer;
                }
                else
                {
                    _currentPos = startPoint + (Quaternion.Euler(0, 90, 0) * direction * offset);
                }

                var gameObject = GameObject.Instantiate(prefab, _currentPos, Quaternion.Euler(rotation));
                var attackComponent = gameObject.AddComponent<Fireball>();
                attackComponent.Init(gameObject, direction);
                _fireballList.Add(attackComponent);
            }
        }

        return _fireballList;
    }

}