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
    public class PagingListObjects : ViewModel.ViewModel
    {
        public Paging Paging { get; set;}

        protected int _selectedIndex;
        public int SelectedIndex
        {
            get => this._selectedIndex;
            set
            {
                this._selectedIndex = value;
                DisplayObjects();
            }
        }

        protected ICommand _previousCommand { get; set; }
        public ICommand PreviousCommand => this._previousCommand ?? (this._previousCommand = new CommandHandler((param) => MyPreviousAction(), () => CanExecute));
        protected ICommand _nextCommand { get; set; }
        public ICommand NextCommand => this._nextCommand ?? (this._nextCommand = new CommandHandler((param) => MyNextAction(), () => CanExecute));
        public PagingListObjects()
        {
            this.Paging = new Paging();
        }
        protected void CalculatePagingInfo(int rowsPerPage, int totalObjects, int selectedIndex = 0)
        {
            // Tinh toan phan trang
            Paging = new Paging()
            {
                RowsPerPage = rowsPerPage,
                CurrentPage = 1,
                TotalPages = totalObjects / rowsPerPage +
                    (((totalObjects % rowsPerPage) == 0) ? 0 : 1)
            };
            this.SelectedIndex = selectedIndex;
        }
        protected void MyNextAction()
        {
            if (this.SelectedIndex < this.Paging.TotalPages - 1)
            {
                this.SelectedIndex += 1;
            }
            else
            {
                MessageBox.Show("Maximum page!");
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
                MessageBox.Show("Minimum page!");
            }
        }

        public bool CanExecute => true;

        public virtual void DisplayObjects() { }
    }
}
