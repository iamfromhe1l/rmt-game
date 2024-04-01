using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    internal abstract class MagicWeapon : Weapon
    {
        protected UpgradableParametr _maxDistance;
        protected UpgradableParametr _Speed;
    }
}
