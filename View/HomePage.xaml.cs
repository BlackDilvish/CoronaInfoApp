using System;
using System.Windows;
using System.Windows.Controls;
using CoronaInfoAppCore.Model; // to delete
using CoronaInfoAppCore.ViewModel;
using System.Linq;

namespace CoronaInfoAppCore.View
{
    public partial class HomePage : UserControl, ISwitchable
    {
        public HomePage()
        {
            InitializeComponent();
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void btnDownload_Click(object sender, RoutedEventArgs e)
        {
            tbDownloaded.Text = string.Empty;

            DataGetter getter = new DataGetter();

            foreach (var cas in getter.GetCases("Italy", CaseType.Deaths, new DateTime(2020, 3, 15)))
                tbDownloaded.Text += $"{cas.CountryName} - {cas.Date} - {cas.NumberOfCases}\n";
        }

        private async void btnReset_Click(object sender, RoutedEventArgs e)
        {
            await DataLoader.UpdateDatabase();
            MessageBox.Show("Baza wirusów została zaktualizowana");
        }

        private void btnGetFromDB_Click(object sender, RoutedEventArgs e)
        {
            tbDownloaded.Text = string.Empty;

            DataGetter getter = new DataGetter();

            foreach (var cas in getter.GetCases("Italy", CaseType.Confirmed, new DateTime(2020, 3, 4)))
                tbDownloaded.Text += $"{cas.CountryName} - {cas.Date} - {cas.NumberOfCases}\n";
        }

        private void btnToChart_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new LineChartPage());
        }
    }
}
