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
            Axe axe = axeCreator.CreateAttack(
                _prefab,
                gameObject.transform.localPosition + Quaternion.Euler(gameObject.transform.eulerAngles) * _offset,
                gameObject.transform.eulerAngles,
                _offset.z,
                gameObject.transform.position + new Vector3(0,_offset.y,0),
                gameObject.transform.eulerAngles.y,
                gameObject.tag
                );
            axe.StartAttack(10);
        }
        public void Awake()
        {
            _damage = ResetUpgradbleParam("damage");
            _timeOut = ResetUpgradbleParam("timeout");
            _enemyMaxCount = ResetUpgradbleParam("enemyMaxCount");
            _distance = ResetUpgradbleParam("distance");
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
