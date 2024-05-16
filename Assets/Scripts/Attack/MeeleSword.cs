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
        public override void Attack()
        {
            Sword sword = swordCreator.CreateAttack(
                _prefab,
                gameObject.transform.localPosition + Quaternion.Euler(gameObject.transform.eulerAngles) * _offset,
                gameObject.transform.eulerAngles
                );
            sword.StartAttack(10);
        }
        public void Awake()
        {
            _damage._current = WeaponConfig.swordLevels["damage"][0];
            _damage._currentLvl = 0;
            _damage._lvlsDictionary = WeaponConfig.swordLevels["damage"];
        }
        public override UpgradableParametr Upgrade(string param)
        {
            throw new NotImplementedException();
        }
    }
}
