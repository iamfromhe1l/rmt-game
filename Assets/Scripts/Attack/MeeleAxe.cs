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
    internal class MeeleAxe : MeeleWeapon
    {
        protected UpgradableParametr _enemyMaxCount { get; set; }
        [SerializeField] protected GameObject _prefab;
        [SerializeField] protected Vector3 _offset;
        AxeCreator axeCreator = new();
        public override string _animationName { get { return "AxeAttack"; } }
        public override void Attack()
        {
            if (_isReloading) return;
            Axe axe = axeCreator.CreateAttack(
                _prefab,
                gameObject.transform.localPosition + Quaternion.Euler(gameObject.transform.eulerAngles) * _offset,
                gameObject.transform.eulerAngles,
                _offset.z,
                gameObject.transform.position + new Vector3(0,_offset.y,0),
                gameObject.transform.eulerAngles.y,
                gameObject.tag
                );
            axe.StartAttack(_damage._current, _enemyMaxCount._current);
            _isReloading = true;
        }
        public void Update() 
        {
            if (_isReloading)
            {
                _currentTimeOut -= Time.deltaTime;
                if ( _currentTimeOut <= 0f )
                {
                    _currentTimeOut = _timeOut._current;
                    _isReloading = false;
                }
               // Debug.Log(_currentTimeOut);
            }
        }
        public void Awake()
        {
            _damage = ResetUpgradbleParam("damage");
            _timeOut = ResetUpgradbleParam("timeout");
            _enemyMaxCount = ResetUpgradbleParam("enemyMaxCount");
            _distance = ResetUpgradbleParam("distance");
            _currentTimeOut = _timeOut._current;
        }
        private UpgradableParametr ResetUpgradbleParam(string perString)
        {
            UpgradableParametr perParam = new();
            perParam._current = WeaponConfig.axeLevels[perString][0];
            perParam._currentLvl = 0;
            perParam._lvlsDictionary = WeaponConfig.axeLevels[perString];
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
            else if (param == "enemyMaxCount")
            {
                return UpgradeByParam(_enemyMaxCount);
            }
            else if (param == "distance")
            {
                return UpgradeByParam(_distance);
            }
            throw new NotImplementedException();
        }
    }
}
