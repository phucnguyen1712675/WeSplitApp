using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WeSplitApp.View;


namespace WeSplitApp.Utils
{
     public class PagingListObjects: ViewModel.ViewModel
    {
        public PagingListObjects()
        {
            this._paging = new Paging();
        }
        protected void CalculatePagingInfo(int rowsPerPage, int totalObjects)
        {
            // Tinh toan phan trang
            Paging = new Paging()
            {
                RowsPerPage = rowsPerPage,
                CurrentPage = 1,
                TotalPages = totalObjects / rowsPerPage +
                    (((totalObjects % rowsPerPage) == 0) ? 0 : 1)
            };
            this.SelectedIndex = 0;
        }

        protected Paging _paging;

        public Paging Paging
        {
            get => this._paging;
            set
            {
                this._paging = value;
                OnPropertyChanged();
            }
        }

        protected int _selectedIndex;
        public int SelectedIndex
        {
            get => this._selectedIndex;
            set
            {
                this._selectedIndex = value;
                OnPropertyChanged();
                DisplayMembers();
            }
        }

        protected ICommand _previousCommand { get; set; }
        public ICommand PreviousCommand => this._previousCommand ?? (this._previousCommand = new CommandHandler(() => MyPreviousAction(), () => CanExecute));
        protected ICommand _nextCommand { get; set; }
        public ICommand NextCommand => this._nextCommand ?? (this._nextCommand = new CommandHandler(() => MyNextAction(), () => CanExecute));

        protected void MyNextAction()
        {
            if (this.SelectedIndex < this._paging.TotalPages - 1)
            {
                this.SelectedIndex += 1;
            }
            else
            {
                MessageBox.Show("Maximum page!", "Reach Maximum page", MessageBoxButton.OK);
            }
        }

        protected void MyPreviousAction()
        {
            if (this.SelectedIndex > 0)
            {
                this.SelectedIndex -= 1;
            }
            else
            {
                MessageBox.Show("Minimum page!", "Reach Minimum page", MessageBoxButton.OK);
            }
        }

        public bool CanExecute
        {
            get
            {
                return true;
            }
        }

        public virtual void DisplayMembers() { }
    }
}
