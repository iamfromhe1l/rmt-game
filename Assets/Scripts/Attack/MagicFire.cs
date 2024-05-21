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
            if (_isReloading) return;
            Fireball fireball = fireballCreator.CreateAttack(   
                _prefab,
                gameObject.transform.localPosition + Quaternion.Euler(gameObject.transform.eulerAngles) * _offset, 
                gameObject.transform.eulerAngles, 
                gameObject.transform.forward);
            fireball.StartAttack(_damage._current);
            _isReloading = true;
        }

        public void Awake()
        {
            _damage = ResetUpgradbleParam("damage");
            _timeOut = ResetUpgradbleParam("timeout");
            _speed = ResetUpgradbleParam("speed");
            _fireballCount = ResetUpgradbleParam("count");
            _currentTimeOut = _timeOut._current;
        }
        public void Update()
        {
            if (_isReloading)
            {
                _currentTimeOut -= Time.deltaTime;
                if (_currentTimeOut <= 0f)
                {
                    _currentTimeOut = _timeOut._current;
                    _isReloading = false;
                }
                Debug.Log(_currentTimeOut);
            }
        }
        private UpgradableParametr ResetUpgradbleParam(string perString)
        {
            UpgradableParametr perParam = new();
            perParam._current = WeaponConfig.fireLevels[perString][0];
            perParam._currentLvl = 0;
            perParam._lvlsDictionary = WeaponConfig.fireLevels[perString];
            return perParam;
        }
        public override UpgradableParametr Upgrade(string param)
        {
            if (param == "damage")
            {
                return UpgradeByParam(_damage);
            } else if (param == "count")
            {
                return UpgradeByParam(_fireballCount);
            } else if (param == "timeout")
            {
                return UpgradeByParam(_timeOut);
            }
            else if (param == "speed")
            {
                return UpgradeByParam(_speed);
            }
            throw new NotImplementedException();
        }
    }
}
