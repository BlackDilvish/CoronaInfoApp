using CoronaInfoAppCore.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using CoronaInfoAppCore.View;

namespace CoronaInfoAppCore
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Switcher.pageSwitcher = this;
            Switcher.Switch(new HomePage());
        }

        public void Navigate(UserControl nextPage)
        {
            Content = nextPage;
        }

        public void Navigate(UserControl nextPage, object state)
        {
            Content = nextPage;
            ISwitchable s = nextPage as ISwitchable;

            if (s != null)
                s.UtilizeState(state);
            else
                throw new ArgumentException("NextPage is not ISwitchable! "
                  + nextPage.Name.ToString());
        }
    }
}
