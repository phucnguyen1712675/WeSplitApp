﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSplitApp.Utils
{
    public class Paging : ViewModel.ViewModel
    {
        public int CurrentPage { get; set; }
        public int RowsPerPage { get; set; } = 4;

        private int _totalPages;
        public int TotalPages
        {
            get => _totalPages; 
            set
            {
                _totalPages = value;
                Pages = new ObservableCollection<PageInfo>();
                for (int i = 1; i <= _totalPages; i++)
                {
                    Pages.Add(new PageInfo()
                    {
                        Page = i,
                        TotalPages = _totalPages
                    });
                }
            }
        }
        public ObservableCollection<PageInfo> Pages { get; set; }
    }
}
