using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    internal class MagicFire : MagicWeapon
    {
        protected UpgradableParametr _fireballCount;
        [SerializeField] protected GameObject _prefab;
        [SerializeField] protected Vector3 _offset;
        FireballCreator fireballCreator = new FireballCreator();
        private List<Fireball> _fireballList = new();
        public override void Attack()
        {
            if (_isReloading) return;
            _fireballList = fireballCreator.CreateAttack(   
                _prefab,
                gameObject.transform.localPosition + Quaternion.Euler(gameObject.transform.eulerAngles) * _offset, 
                gameObject.transform.eulerAngles, 
                gameObject.transform.forward,
                _fireballCount._current);
            foreach (Fireball fireball in _fireballList)
            {
                fireball.StartAttack(_damage._current, _speed._current);
            }
            _fireballList.Clear();
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
                    _currentTimeOut = 1f;
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
