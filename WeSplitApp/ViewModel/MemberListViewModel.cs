using System.Collections.ObjectModel;
using System.Linq;
using WeSplitApp.Utils;
using WeSplitApp.View;

namespace WeSplitApp.ViewModel
{
    public class MemberListViewModel : PagingListObjects
    {
        private ObservableCollection<MEMBER> _mEMBERS;
        public ObservableCollection<MEMBER> MEMBERS
        {
            get => this._mEMBERS;
            set
            {
                this._mEMBERS = value;
                OnPropertyChanged();
            }
        }

        public PagingListObjects PagingListObjects { get; set; }

        private static MemberListViewModel instance = null;
        public static MemberListViewModel Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new MemberListViewModel();
                }
                return instance;
            }
        }

        private MemberListViewModel()
        {
            this.MEMBERS = new ObservableCollection<MEMBER>(HomeScreen.GetDatabaseEntities().MEMBERS.ToList());
            //TODO read from data.config
            int RowsPerPage = 5;

            CalculatePagingInfo(RowsPerPage, MEMBERS.Count);
            DisplayObjects();
        }

        #region Paging
        private ObservableCollection<MEMBER> _toShowItems;
        public ObservableCollection<MEMBER> ToShowItems
        {
            get => this._toShowItems;
            set
            {
                this._toShowItems = value;
                OnPropertyChanged();
            }
        }

        public override void DisplayObjects()
        {
            var page = this.SelectedIndex + 1;
            var skip = (page - 1) * this._paging.RowsPerPage;
            var take = this._paging.RowsPerPage;

            this.ToShowItems = new ObservableCollection<MEMBER>(this.MEMBERS.Skip(skip).Take(take));
        }


        public static bool getNewRowPerPage(int RowsPerPage) // được gọi trong setting
        {
            if(RowsPerPage > instance.MEMBERS.Count)
            {
                return false;
            }
            instance.CalculatePagingInfo(RowsPerPage, instance.MEMBERS.Count);

            return true;
        }

        public static int getRowsPerPage() //gọi lúc tắt app để lưu setting paging
        {
            return instance.Paging.RowsPerPage;
        }
        #endregion

        public static void updateList(MEMBER member)
        {
            if(instance != null)
            {
                instance.MEMBERS.Add(member);
                instance.DisplayObjects();
            }
        }
    }
}
