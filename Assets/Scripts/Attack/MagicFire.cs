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
            fireball.StartAttack(10);
        }

        public void Awake()
        {
            _damage._current = WeaponConfig.fireLevels["damage"][0];
            _damage._currentLvl = 0;
            _damage._lvlsDictionary = WeaponConfig.fireLevels["damage"];

            //_fireballCount._current = WeaponConfig.fireLevels["fireballCount"][0];
            //_fireballCount._currentLvl = 0;
            //_fireballCount._lvlsDictionary = WeaponConfig.fireLevels["fireballCount"];
        }

        public override UpgradableParametr Upgrade(string param)
        {
            if (param == "damage")
            {
                if (_damage._currentLvl <= 2)
                {
                    _damage._currentLvl += 1;
                    _damage._current = _damage._lvlsDictionary[_damage._currentLvl];
                    return _damage;
                }
            }
            return _damage;
        }
    }
}
