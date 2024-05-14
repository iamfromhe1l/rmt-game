using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    internal class MagicFire : MagicWeapon
    {
        protected UpgradableParametr _fireballCount;
        [SerializeField] protected GameObject _prefab;
        [SerializeField] protected Vector3 _offset;
        FireballCreator fireballCreator = new FireballCreator();
        public override void Attack()
        {
            Fireball fireball = fireballCreator.CreateAttack(
                _prefab,
                gameObject.transform.localPosition + Quaternion.Euler(gameObject.transform.eulerAngles) * _offset, 
                gameObject.transform.eulerAngles, 
                gameObject.transform.forward);
            fireball.StartAttack(_damage._current);
        }
        /*public void Awake()
        {
            //_prefab = Resources.Load<GameObject>("Sphere");
        }*/
        public override UpgradableParametr Upgrade(string lvl)
        {
            throw new NotImplementedException();
        }
    }
}
