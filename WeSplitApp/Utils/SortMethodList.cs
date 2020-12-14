using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSplitApp.Utils
{
    public class SortMethodList : PagingListObjects
    {
        public SortMethodList()
        {
            this.MySort = new Dictionary<string, Delegate>();
        }
        public Dictionary<string, Delegate> MySort { get; set; }

        protected virtual void SetSort(string method) { }
    }
}
