using Assets.Scripts.Attack;
using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    internal class MeeleSword : MeeleWeapon
    {
        [SerializeField] protected GameObject _prefab;
        [SerializeField] protected Vector3 _offset;
        SwordCreator swordCreator = new();
        public override string _animationName { get { return "SwordAttack"; } }
        public override void Attack()
        {
            if (_isReloading) return;
            Sword sword = swordCreator.CreateAttack(
                _prefab,
                gameObject.transform.localPosition + Quaternion.Euler(gameObject.transform.eulerAngles) * _offset,
                gameObject.transform.eulerAngles
                );
            sword.StartAttack(10);
            _isReloading = true;
        }
        public void Awake()
        {
            _damage = ResetUpgradbleParam("damage");
            _timeOut = ResetUpgradbleParam("timeout");
            _distance = ResetUpgradbleParam("distance");
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
            perParam._current = WeaponConfig.swordLevels[perString][0];
            perParam._currentLvl = 0;
            perParam._lvlsDictionary = WeaponConfig.swordLevels[perString];
            return perParam;
        }
        public override UpgradableParametr Upgrade(string param)
        {
            if (param == "damage")
            {
                return UpgradeByParam(_damage);
            }
            else if (param == "timeout")
            {
                return UpgradeByParam(_timeOut);
            }
            else if (param == "distance")
            {
                return UpgradeByParam(_distance);
            }
            throw new NotImplementedException();
        }
    }
}
