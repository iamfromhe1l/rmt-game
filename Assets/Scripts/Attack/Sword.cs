using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;
namespace Assets.Scripts.Attack
{
    internal class Sword : MonoBehaviour
    {
        protected GameObject _attackArea;
        protected int _damage = 0;
        protected bool isCoroutineRunning = false;
        public List<IDamageable> Damageables { get; } = new();

        public void StartAttack(int damage)
        {
            _damage = damage;
            StartCoroutine(SwordCoroutine());
        }
        public void Init(GameObject prefab)
        {
            _attackArea = prefab;
        }
        private IEnumerator SwordCoroutine()
        {
            yield return new WaitForSeconds(1f);
            Destroy(this.gameObject);
            yield break;
        }
        public void OnTriggerEnter(Collider other)
        {
            var damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            { 
                damageable.Damage(_damage);
            }
        }
    }
}
