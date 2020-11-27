using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSplitApp.Utils
{
    public class MyRandom
    {
        private static MyRandom _instance = null;
        private Random _rng;

        public static MyRandom Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MyRandom();
                }
                return _instance;
            }
        }

        public int Next(int ceiling)
        {
            int value = _rng.Next(ceiling);
            return value;
        }

        private MyRandom() => _rng = new Random();
    }
}
