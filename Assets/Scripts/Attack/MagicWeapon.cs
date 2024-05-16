using UnityEngine;

namespace Assets.Scripts
{
    internal abstract class MagicWeapon : Weapon
    {
        protected UpgradableParametr _maxDistance;
        protected UpgradableParametr _Speed;
        public override string _animationName { get { return "MagicAttack"; } }
        /*protected Vector3 _startPoint;
        protected Vector3 _offset;*/
        /*protected void Init(Vector3 startPoint, Vector3 offset)
        {
            _startPoint = startPoint;
            _offset = offset;
        }*/
    }
}
