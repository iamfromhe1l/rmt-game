using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    internal class Meele_Axe : MeeleWeapon
    {
        protected UpgradableParametr _enemyMaxCount;
        public override void Attack(IDamageable damageable)
        {
            throw new NotImplementedException();
        }

        public override UpgradableParametr Upgrade(string lvl)
        {
            throw new NotImplementedException();
        }
    }
}
