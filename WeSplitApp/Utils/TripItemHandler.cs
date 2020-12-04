using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSplitApp.Utils
{
    public class TripItemHandler
    {
        public TripItemHandler()
        {
            Items = new List<TRIP>();
        }
        public TripItemHandler(List<TRIP> trip)
        {
            Items = trip;
        }

        public List<TRIP> Items { get; private set; }

        public void Add(TRIP item)
        {
            Items.Add(item);
        }
    }
}
