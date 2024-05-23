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
        protected float _fireballCount = 0;
        protected float _speed = 0;
        [SerializeField] GameObject _explosionSystem;
        public List<IDamageable> Damageables { get; } = new();
        private IEnumerator _resizeCoroutine;
        private Coroutine _fireballCoroutine;

        public void StartAttack(int damage, float speed)
        {
            _speed = speed;
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
            yield return new WaitForSeconds(0.4f);
            float time = 0;
            while (time < 4f)
            {
                _fireball.transform.position += _playerDirection * Time.deltaTime * _speed; //_Speed
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
