using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class UpgradableParametr
    {
        public int _current {  get; set; }
        public int _currentLvl { get; set; }
        public Dictionary<int, int> _lvlsDictionary { get; set; } 
    }
}
