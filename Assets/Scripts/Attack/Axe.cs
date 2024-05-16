using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Attack
{
    internal class Axe : MonoBehaviour
    {
        protected GameObject _attackArea;
        protected int _damage = 0;
        protected float _radius = 0;
        protected Vector3 _center = Vector3.zero;
        protected float _angle = 0;
        protected string _tag = string.Empty;
        private List<IDamageable> Damageables { get; } = new();

        public void StartAttack(int damage)
        {
            _damage = damage;
            StartCoroutine(AxeCoroutine());
        }
        public void Init(GameObject prefab, float radius, Vector3 center, float angle, string tagEnemy)
        {
            _attackArea = prefab;
            _radius = radius;
            _center = center;
            _angle = angle + 90;
            _tag = tagEnemy;
        }
        private IEnumerator AxeCoroutine()
        {
            yield return new WaitForSeconds(0.75f);
            float startAngle = _angle;
            while (_angle >= startAngle - 360)
            {
                _attackArea.transform.position = new Vector3(
                    _center.x + _radius * Mathf.Cos(_angle * Mathf.Deg2Rad), 
                    _attackArea.transform.position.y, 
                    _center.z + _radius * Mathf.Sin(_angle * Mathf.Deg2Rad));
                _attackArea.transform.LookAt(_center);
                _angle -= 720 * Time.deltaTime; //Speed
                yield return null;
            }
            Damageables.Clear();
            Destroy(this.gameObject);
            yield break;
        }
        public void OnTriggerEnter(Collider other)
        {
            var damageable = other.GetComponent<IDamageable>();
            if (damageable != null && other.tag != _tag && !Damageables.Contains(damageable))
            {
                Damageables.Add(damageable);
                damageable.Damage(_damage);
            }
        }
    }
}
