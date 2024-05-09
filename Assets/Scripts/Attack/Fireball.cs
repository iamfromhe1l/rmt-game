using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    internal class Fireball : MonoBehaviour
    {
        protected GameObject _fireball;
        protected Vector3 _startPoint;
        protected Vector3 _playerDirection;
        public void StartAttack(int damage)
        {
            StartCoroutine(FireballCoroutine());
        }
        public void Init(Vector3 startPoint, GameObject prefab, Vector3 playerDirection)
        {
            _startPoint = startPoint;
            _fireball = prefab;
            _playerDirection = playerDirection;
        }
        private IEnumerator FireballCoroutine()
        {
            float time = 0;
            while (time < 10f)
            {
                _fireball.transform.position += _playerDirection * Time.deltaTime * 15; //_Speed
                time += Time.deltaTime;
                yield return null;
            }
            Destroy(_fireball);
            yield break;
        }
    }
}
