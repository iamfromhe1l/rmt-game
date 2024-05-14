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
        protected UpgradableParametr _timeOut;
        protected UpgradableParametr _damage;
        public string _animationName { get; }
        public abstract UpgradableParametr Upgrade(string lvl);
        protected void RegisterHandler(AnimatorHandler deleagate)
        {
            _changeAnimation = deleagate;
        }
        public abstract void Attack();
    }
}
