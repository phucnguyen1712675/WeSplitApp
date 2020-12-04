using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using WeSplitApp.Utils;

namespace WeSplitApp.ViewModel
{
    public class AboutUsViewModel : ViewModel
    {
        private string _text;
        public string Text
        {
            get => this._text;
            set
            {
                this._text = value;
                OnPropertyChanged();
            }
        }
        private ICommand _testCommand { get; set; }
        public ICommand test => this._testCommand ?? (this._testCommand = new CommandHandler(() => MyTestAction(), () => CanExecute));

        public AboutUsViewModel() { }

    }
}
