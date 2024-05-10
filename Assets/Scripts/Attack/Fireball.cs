using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    internal class Fireball : MonoBehaviour
    {
        protected GameObject _fireball;
        protected Vector3 _startPoint;
        protected Vector3 _playerDirection;
        protected int _damage = 0;
        protected DamageArea damageArea;
        public List<IDamageable> Damageables { get; } = new();
        
        public void StartAttack(int damage)
        {
            _damage = damage;
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
            Debug.Log(111);
            var damagable = other.GetComponent<IDamageable>();
            if (damagable != null)
            {
                Damageables.Add(damagable);
            }
        }
        public void OnTriggerExit(Collider other)
        {
            var damageable = other.GetComponent<IDamageable>();
            if (damageable != null && Damageables.Contains(damageable))
            {
                damageable.Damage(_damage);
                Damageables.Remove(damageable);
            }
            StopCoroutine(FireballCoroutine());
            Destroy(this.gameObject);
        }
    }
}
