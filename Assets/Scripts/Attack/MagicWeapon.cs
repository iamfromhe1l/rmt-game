using UnityEngine;

namespace Assets.Scripts
{
    internal abstract class MagicWeapon : Weapon
    {
        protected UpgradableParametr _maxDistance { get; set; } = new();
        protected UpgradableParametr _speed { get; set; } = new();
        public override string _animationName { get { return "MagicAttack"; } }
    }
}
