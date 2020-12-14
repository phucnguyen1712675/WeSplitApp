using MaterialDesignThemes.Wpf;
using System.Windows;
using WeSplitApp.View;
using PaletteHelper = MaterialDesignExtensions.Themes.PaletteHelper;

namespace WeSplitApp.ViewModel
{
    public class HomeScreenViewModel : ViewModel
    {
        private PaletteHelper m_paletteHelper;

        private bool m_isDarkTheme;
        public bool IsDarkTheme
        {
            get => this.m_isDarkTheme;

            set
            {
                if (m_isDarkTheme != value)
                {
                    m_isDarkTheme = value;

                    //OnPropertyChanged(nameof(IsDarkTheme));

                    SetTheme(m_isDarkTheme);
                }
            }
        }
        public HomeScreenViewModel()
        {
            m_paletteHelper = new PaletteHelper();
            m_isDarkTheme = m_paletteHelper.IsDarkTheme();
        }

        private void SetTheme(bool isDarkTheme)
        {
            m_paletteHelper.SetLightDark(m_isDarkTheme);

            ((HomeScreen)Application.Current.MainWindow).DialogHost.DialogTheme = isDarkTheme ? BaseTheme.Dark : BaseTheme.Light;
        }
    }
}
