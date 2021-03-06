﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using WeSplitApp.Utils;
using WeSplitApp.View;
using WeSplitApp.View.Controls;

namespace WeSplitApp.ViewModel
{
    public class MemberListViewModel : SortMethodList
    {
        private ObservableCollection<MEMBER> _mEMBERS;
        public ObservableCollection<MEMBER> MEMBERS
        {
            get => this._mEMBERS;
            set
            {
                this._mEMBERS = value;
            }
        }

        private static MemberListViewModel instance = null;
        public static MemberListViewModel Instance => instance ?? (instance = new MemberListViewModel());

        private MemberListViewModel()
        {
            MySort = new Dictionary<string, Delegate> {
                  { "Mặc định", new Func<List<MEMBER>>(SetDefaultPosition)},
                { "Tên tăng dần", new Func<List<MEMBER>>(SetAscendingPositionAccordingToName)},
                { "Tên giảm đần", new Func<List<MEMBER>>(SetDescendingPositionAccordingToName)}
            };

            this.MEMBERS = new ObservableCollection<MEMBER>((HomeScreen.GetDatabaseEntities().MEMBERS).ToList());
            this.searchMEMBERS = this.MEMBERS;
        }

        public int GetMaximum()
        {
            return MEMBERS.Count();
        }

        #region sort
        protected override void SetSort(string method)
        {
            if (MySort.ContainsKey(method))
            {
                List<MEMBER> resultSort =(List<MEMBER>)MySort[method].DynamicInvoke();
                MEMBERS = new ObservableCollection<MEMBER>(resultSort);
                if(searchMEMBERS != MEMBERS)
                {
                    searchMember_ByName();
                }
                DisplayObjects();
            }
        }

        private List<MEMBER> SetDescendingPositionAccordingToName()
        {
            return MEMBERS.OrderByDescending(c => c.NAME).ToList();
        }

        private List<MEMBER> SetAscendingPositionAccordingToName()
        {
            return MEMBERS.OrderBy(c => c.NAME).ToList();
        }

        private List<MEMBER> SetDefaultPosition()
        {
            return MEMBERS.OrderBy(c => c.MEMBER_ID).ToList();
        }

        public List<string> getSortMethod()
        {
            return MySort.Keys.ToList();
        }

        public void MakeSort(string method)
        {
            SetSort(method);
        }

        public void MakeSort(int method)
        {
            MakeSort(MySort.Keys.ToList()[method]);
        }

        #endregion

        #region Paging
        private ObservableCollection<MEMBER> _toShowItems;
        public ObservableCollection<MEMBER> ToShowItems
        {
            get => this._toShowItems;
            set
            {
                this._toShowItems = value;
            }
        }

        public override void DisplayObjects()
        {
            var page = this.SelectedIndex + 1;
            var skip = (page - 1) * this.Paging.RowsPerPage;
            var take = this.Paging.RowsPerPage;
            //TODO test Paging.Pages
            var temp= Paging.TotalPages;
            this.ToShowItems = new ObservableCollection<MEMBER>(this.searchMEMBERS.Skip(skip).Take(take));
        }

        public bool getNewRowPerPage(int RowsPerPage) // được gọi trong setting
        {
            if(RowsPerPage > searchMEMBERS.Count)
            {
                return false;
            }
            CalculatePagingInfo(RowsPerPage, searchMEMBERS.Count);
            SelectedIndex = 0;
            DisplayObjects();
            return true;
        }
        #endregion

        public void updateList(MEMBER member)
        {
            if (instance != null)
            {
                MEMBERS.Add(member);
                DisplayObjects();
                if (MEMBERS.Count % Paging.RowsPerPage == 1)
                    CalculatePagingInfo(Paging.RowsPerPage, MEMBERS.Count, SelectedIndex);
            }
        }
        private ObservableCollection<MEMBER> _searchMEMBERS;
        public ObservableCollection<MEMBER> searchMEMBERS
        {
            get => this._searchMEMBERS;
            set
            {
                this._searchMEMBERS = value;
            }
        }
        public void searchMember_ByName()
        {
            string request = HomeScreen.GetHomeScreenInstance().SearchTextBox.Text;
            List<MEMBER> memberList;
            if (request.Length <= 0)
            {
                memberList = (MEMBERS).ToList();
            }
            else
            {
                //search by Name
                var requestText = convertUnicode.convertToUnSign(request.Trim().ToLower());
                memberList = (MEMBERS.AsEnumerable().Where(mem => convertUnicode.convertToUnSign(mem.NAME.Trim().ToLower()).Contains(requestText))).ToList();

            }
            searchMEMBERS = new ObservableCollection<MEMBER>(memberList);
            CalculatePagingInfo(Paging.RowsPerPage, searchMEMBERS.Count);
            DisplayObjects();
        }
    }
}
