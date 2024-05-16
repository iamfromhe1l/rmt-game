using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts
{
    internal class Fireball : MonoBehaviour
    {
        protected GameObject _fireball;
        protected Vector3 _playerDirection;
        protected int _damage = 0;
        [SerializeField] GameObject _explosionSystem;
        public List<IDamageable> Damageables { get; } = new();
        private IEnumerator _resizeCoroutine;
        private Coroutine _fireballCoroutine;

        public void StartAttack(int damage)
        {
            Debug.Log(damage);
            _damage = damage;
            _fireballCoroutine = StartCoroutine(FireballCoroutine());
        }
        public void Init(GameObject prefab, Vector3 playerDirection)
        {
            _fireball = prefab;
            _playerDirection = playerDirection;
        }
        private IEnumerator FireballCoroutine()
        {
            float time = 0;
            while (time < 2f)
            {
                _fireball.transform.position += _playerDirection * Time.deltaTime * 15; //_Speed
                time += Time.deltaTime;
                yield return null;
            }
            Destroy(this.gameObject);
            yield break;
        }
        public void OnTriggerEnter(Collider other)
        {
            var damagable = other.GetComponent<IDamageable>();
            if (damagable != null)
            {
                damagable.Damage(_damage);
            }
            Destroy(this.gameObject);
        }
    }
}
