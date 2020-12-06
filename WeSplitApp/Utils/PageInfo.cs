using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSplitApp.Utils
{
    public class PageInfo : ViewModel.ViewModel
    {
        public int Page { get; set; }
        public int TotalPages { get; set; }
    }
}
