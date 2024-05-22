using System;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts
{
    internal abstract class MeeleWeapon : Weapon
    {
        protected UpgradableParametr _distance { get; set; } = new();
    }
}
