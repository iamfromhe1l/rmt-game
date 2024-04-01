using Assets.Scripts.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    internal abstract class Weapon
    {
        protected delegate Animator _changeAnim();
        public string _name { get;}
        protected bool _isReloading;
        protected UpgradableParametr _timeOut;
        protected UpgradableParametr _damage;
        public string _animationName { get; }
        public  abstract void Attack(IDamageable damageables);
        public abstract UpgradableParametr Upgrade(string lvl);
    }
}
