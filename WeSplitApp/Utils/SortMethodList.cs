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
            this._mySort = new Dictionary<string, Delegate>();
        }

        private Dictionary<string, Delegate> _mySort;
        public Dictionary<string, Delegate> MySort
        {
            get => this._mySort;
            set
            {
                this._mySort = value;
                OnPropertyChanged();
            }
        }

        protected virtual void SetSort(string method) { }
    }
}
