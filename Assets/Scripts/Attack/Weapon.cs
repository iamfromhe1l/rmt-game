using Assets.Scripts.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    internal abstract class Weapon: MonoBehaviour
    {
        public string _name { get;}
        protected bool _isReloading = false;
        public bool GetIsReloading { get { return _isReloading; } }
        protected UpgradableParametr _timeOut { get; set; } = new();
        protected UpgradableParametr _damage { get; set; } = new();
        public abstract string _animationName { get; }
        protected float _currentTimeOut;
        public float GetCurrentTimeOut()
        {
            return _currentTimeOut;
        }
        public float GetTimeOut()
        {
            return _timeOut._current;
        }
        public abstract UpgradableParametr Upgrade(string param);
        protected UpgradableParametr UpgradeByParam(UpgradableParametr perParam)
        {
            perParam._currentLvl += 1;
            perParam._current = perParam._lvlsDictionary[perParam._currentLvl];
            return perParam;
        }
        public abstract void Attack();
    }
}
