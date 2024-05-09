using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    internal class MagicFire : MagicWeapon
    {
        protected UpgradableParametr _fireballCount;
        protected GameObject _prefab;
        [SerializeField] protected Vector3 _offset; 
        public override void Attack(int damage)
        {
            FireballCreator fireballCreator = new FireballCreator();
            Fireball fireball = fireballCreator.CreateAttack(_prefab,
                gameObject.transform.localPosition + Quaternion.Euler(gameObject.transform.eulerAngles) * _offset, 
                gameObject.transform.eulerAngles, 
                gameObject.transform.forward);
            fireball.StartAttack(damage);
        }
        public void Awake()
        {
            _prefab = Resources.Load<GameObject>("Sphere");
        }
        public override UpgradableParametr Upgrade(string lvl)
        {
            throw new NotImplementedException();
        }
    }
}
