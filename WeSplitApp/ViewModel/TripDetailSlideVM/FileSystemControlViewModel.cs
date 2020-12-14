using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSplitApp.ViewModel.TripDetailSlideVM
{
    public abstract class FileSystemControlViewModel : ViewModel
    {
        public bool CreateNewDirectoryEnabled { get; set; }
        public string SelectedAction { get; set; }
        public bool ShowHiddenFilesAndDirectories { get; set; }
        public bool ShowSystemFilesAndDirectories { get; set; }
        public bool SwitchPathPartsAsButtonsEnabled { get; set; }
        public FileSystemControlViewModel()
            : base()
        {
            this.SelectedAction = null;
            this.ShowHiddenFilesAndDirectories = false;
            this.ShowSystemFilesAndDirectories = false;
            this.CreateNewDirectoryEnabled = false;
            this.SwitchPathPartsAsButtonsEnabled = false;
        }
    }
}
