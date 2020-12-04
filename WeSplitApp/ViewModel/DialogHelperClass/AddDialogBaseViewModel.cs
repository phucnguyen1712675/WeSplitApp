using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeSplitApp.Utils;

namespace WeSplitApp.ViewModel.DialogHelperClass
{
    abstract class AddDialogBaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
           => args => PropertyChanged?.Invoke(this, args);

        public string type { get; set; }

        private ICommand _yesCommand { get; set; }
        public ICommand YesCommand => this._yesCommand ?? (this._yesCommand = new CommandHandler(() => YesCommandAction(), () => CanExecute));

        private ICommand _noCommand { get; set; }
        public ICommand NoCommand => this._noCommand ?? (this._noCommand = new CommandHandler(() => NoCommandAction(), () => CanExecute));

        public virtual void NoCommandAction()
        {
        }

        public virtual void YesCommandAction()
        {
        }

        public bool CanExecute { get { return true; } }

    }
}
