using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSplitApp.View;

namespace WeSplitApp.ViewModel
{
    public class MemberListViewModel : ViewModel
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
        //public ObservableCollection<MEMBER> MEMBERS { get; set; }
        private static MemberListViewModel instance { get; set; }

        public static void updateList(MEMBER member)
        {
            if(instance != null)
                instance.MEMBERS.Add(member);
        }

        public MemberListViewModel(ObservableCollection<MEMBER> MEMBERS) {
            //MEMBERS = new ObservableCollection<MEMBER>(HomeScreen.homeScreen.database.MEMBERS.ToList());
            this.MEMBERS = MEMBERS;
            instance = this;
        }
    }
}
