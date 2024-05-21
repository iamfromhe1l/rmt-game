using Assets.Scripts.Attack;
using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts
{
    internal class MagicWind : MagicWeapon
    {
        protected UpgradableParametr _resDistance;
        [SerializeField] protected GameObject _prefab;
        [SerializeField] protected Vector3 _offset;
        WindCreator windCreator = new WindCreator();
        public override void Attack()
        {
            if (_isReloading) return;
            Wind wind = windCreator.CreateAttack(
                _prefab,
                gameObject.transform.localPosition + Quaternion.Euler(gameObject.transform.eulerAngles) * _offset,
                gameObject.transform.eulerAngles,
                gameObject.transform.forward);
            wind.StartAttack(_damage._current, _speed._current, _resDistance._current);
            _isReloading = true;
        }
        public void Awake()
        {
            _damage = ResetUpgradbleParam("damage");
            _timeOut = ResetUpgradbleParam("timeout");
            _speed = ResetUpgradbleParam("speed");
            _resDistance = ResetUpgradbleParam("reclining");
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
            perParam._current = WeaponConfig.windLevels[perString][0];
            perParam._currentLvl = 0;
            perParam._lvlsDictionary = WeaponConfig.windLevels[perString];
            return perParam;
        }
        public override UpgradableParametr Upgrade(string param)
        {
            if (param == "damage")
            {
                return UpgradeByParam(_damage);
            }
            else if (param == "reclining")
            {
                return UpgradeByParam(_resDistance);
            }
            else if (param == "timeout")
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
