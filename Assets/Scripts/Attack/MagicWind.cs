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
    internal class MagicWind : MagicWeapon
    {
        protected UpgradableParametr _fireballCount;
        [SerializeField] protected GameObject _prefab;
        [SerializeField] protected Vector3 _offset;
        WindCreator windCreator = new WindCreator();
        public override void Attack()
        {
            Wind wind = windCreator.CreateAttack(
                _prefab,
                gameObject.transform.localPosition + Quaternion.Euler(gameObject.transform.eulerAngles) * _offset,
                gameObject.transform.eulerAngles,
                gameObject.transform.forward);
            wind.StartAttack(10);
        }
        public override UpgradableParametr Upgrade(string lvl)
        {
            throw new NotImplementedException();
        }
    }
}
