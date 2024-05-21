using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Assets.Scripts.Attack
{
    internal class Wind : MonoBehaviour
    {
        protected GameObject _wind;
        protected Vector3 _playerDirection;
        protected int _damage = 0;
        protected float _distance = 0;
        protected float _speed = 0;
        public List<IDamageable> Damageables { get; } = new();

        public void StartAttack(int damage, float speed, float distance)
        {
            _distance = distance;
            _speed = speed;
            _damage = damage;
            StartCoroutine(WindMoveCoroutine());
        }
        public void Init(GameObject prefab, Vector3 playerDirection)
        {
            _wind = prefab;
            _playerDirection = playerDirection;
        }
        private IEnumerator WindMoveCoroutine()
        {
            yield return new WaitForSeconds(0.4f);
            float time = 0;
            while (time < _distance / _speed)
            {
                _wind.transform.position += _playerDirection * Time.deltaTime * _speed; //_Speed
                time += Time.deltaTime;
                yield return null;
            }
            Destroy(this.gameObject);
            yield break;
        }
        private IEnumerator ObjectMove(Rigidbody rigidbody)
        {
            
            rigidbody.AddForce(Vector3.Normalize(_playerDirection) * 15, ForceMode.VelocityChange);
            yield break;
        }
        public void OnTriggerEnter(Collider other)
        {
            var damagable = other.GetComponent<IDamageable>();
            if (damagable != null)
            {
                damagable.Damage(_damage);
            }
            StopCoroutine(WindMoveCoroutine());
            var rigidBody = other.GetComponent<Rigidbody>();
            if (rigidBody != null) StartCoroutine(ObjectMove(rigidBody));
            Destroy(this.gameObject);
        }
    }
}
