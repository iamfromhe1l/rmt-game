using Assets.Scripts.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    internal abstract class Weapon: MonoBehaviour
    {
        protected delegate Animator AnimatorHandler();
        protected AnimatorHandler _changeAnimation;
        public string _name { get;}
        protected bool _isReloading;
        protected UpgradableParametr _timeOut { get; set; } = new();
        protected UpgradableParametr _damage { get; set; } = new();
        public string _animationName { get; }
        public abstract UpgradableParametr Upgrade(string param);
        protected void RegisterHandler(AnimatorHandler deleagate)
        {
            _changeAnimation = deleagate;
        }
        protected UpgradableParametr UpgradeByParam(UpgradableParametr perParam)
        {
            perParam._currentLvl += 1;
            perParam._current = perParam._lvlsDictionary[perParam._currentLvl];
            return perParam;
        }
        public abstract void Attack();
    }
}
