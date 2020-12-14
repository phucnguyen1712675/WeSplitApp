using System;
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
        public ObservableCollection<MEMBER> MEMBERS { get; set; }

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
            return instance.MySort.Keys.ToList();
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
        public ObservableCollection<MEMBER> ToShowItems { get; set; }
        public override void DisplayObjects()
        {
            var page = this.SelectedIndex + 1;
            var skip = (page - 1) * this.Paging.RowsPerPage;
            var take = this.Paging.RowsPerPage;

            this.ToShowItems = new ObservableCollection<MEMBER>(this.MEMBERS.Skip(skip).Take(take));
        }

        public bool getNewRowPerPage(int RowsPerPage) // được gọi trong setting
        {
            if(RowsPerPage > MEMBERS.Count)
            {
                return false;
            }
            CalculatePagingInfo(RowsPerPage, MEMBERS.Count);
            SelectedIndex = 0;
            DisplayObjects();
            return true;
        }
        #endregion

        public void updateList(MEMBER member)
        {
            if (instance != null)
            {
                instance.MEMBERS.Add(member);
                instance.DisplayObjects();
            }
        }

        public void searchMember_ByName()
        {
            string request = HomeScreen.GetHomeScreenInstance().SearchTextBox.Text;
            //MessageBox.Show("Chay duoc roi");
            List<MEMBER> memberList;
            if (request.Length <= 0)
            {
                memberList = (HomeScreen.GetDatabaseEntities().MEMBERS).ToList();
                //this.ToShowItems = new ObservableCollection<TRIP>(all);
            }
            else
            {
                //search by TITLE
                var requestText = convertUnicode.convertToUnSign(request.Trim().ToLower());
                memberList = (HomeScreen.GetDatabaseEntities().MEMBERS.AsEnumerable().Where(mem => convertUnicode.convertToUnSign(mem.NAME.Trim().ToLower()).Contains(requestText))).ToList();

                //MessageBox.Show(b[0].TITTLE);
                //this.ToShowItems = new ObservableCollection<TRIP>(tripList);
            }
            instance.MEMBERS = new ObservableCollection<MEMBER>(memberList);
            instance.CalculatePagingInfo(instance.Paging.RowsPerPage, instance.MEMBERS.Count);
            instance.DisplayObjects();
        }
    }
}
