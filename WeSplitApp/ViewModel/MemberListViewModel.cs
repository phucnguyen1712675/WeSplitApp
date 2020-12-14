using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using WeSplitApp.Utils;
using WeSplitApp.View;

namespace WeSplitApp.ViewModel
{
    public class MemberListViewModel : PagingListObjects
    {
        public ObservableCollection<MEMBER> MEMBERS { get; set; }
        public PagingListObjects PagingListObjects { get; set; }
        public ObservableCollection<MEMBER> ToShowItems { get; set; }

        private static MemberListViewModel instance = null;
        public static MemberListViewModel Instance => instance ?? (instance = new MemberListViewModel());

        private MemberListViewModel()
        {
            this.MEMBERS = new ObservableCollection<MEMBER>(HomeScreen.GetDatabaseEntities().MEMBERS.ToList());
            //TODO read from data.config
            int RowsPerPage = 5;

            CalculatePagingInfo(RowsPerPage, MEMBERS.Count);
            DisplayObjects();
        }

        #region Paging

        public override void DisplayObjects()
        {
            var page = this.SelectedIndex + 1;
            var skip = (page - 1) * this.Paging.RowsPerPage;
            var take = this.Paging.RowsPerPage;

            this.ToShowItems = new ObservableCollection<MEMBER>(this.MEMBERS.Skip(skip).Take(take));
        }
        public static bool getNewRowPerPage(int RowsPerPage) // được gọi trong setting
        {
            if (RowsPerPage > instance.MEMBERS.Count)
            {
                return false;
            }
            instance.CalculatePagingInfo(RowsPerPage, instance.MEMBERS.Count);

            return true;
        }
        public static int getRowsPerPage() => instance.Paging.RowsPerPage; //gọi lúc tắt app để lưu setting paging
        #endregion

        public static void updateList(MEMBER member)
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
