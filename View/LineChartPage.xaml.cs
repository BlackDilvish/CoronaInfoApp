using CoronaInfoAppCore.ViewModel;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Linq;
using System.Windows.Media;
using System.Collections.Generic;
using System.Windows;

namespace CoronaInfoAppCore.View
{
    public partial class LineChartPage : UserControl, ISwitchable
    {
        private ChartController ChartController { get; set; }
        private ComboBoxController ComboBoxController { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<int, string> YFormatter { get; set; }

        public LineChartPage()
        {
            InitializeComponent();

            ComboBoxController = new ComboBoxController();

            ComboBoxController.FillCountrySelector(cbxCountrySelection);
            ComboBoxController.FillCaseTypeSelector(cbxCaseTypeSelection);

            ChartController = new ChartController();

            SeriesCollection = new SeriesCollection()
            {
                ChartController.GetSeries("Poland", (int)Model.CaseType.Confirmed),
                ChartController.GetSeries("Poland", (int)Model.CaseType.Deaths)
            };

            Labels = ChartController.GetLabels();
            YFormatter = value => value.ToString();
            DataContext = this;
        }

        public void UtilizeState(object state)
        {
            throw new System.NotImplementedException();
        }

        private void cbxCountrySelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var countryName = cbxCountrySelection.SelectedItem.ToString();
            cbxCaseTypeSelection.IsEnabled = false;

            if (!ComboBoxController.FillProvinceSelector(cbxProvinceSelection, countryName))
                cbxCaseTypeSelection.IsEnabled = true;
        }

        private void cbxProvinceSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbxCaseTypeSelection.IsEnabled = true;
            btnCreateChart.IsEnabled = false;
        }

        private void cbxDataTypeSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnCreateChart.IsEnabled = true;
        }

        private void btnCreateChart_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SeriesCollection.Clear();

            var country = cbxCountrySelection.SelectedItem;
            var province = cbxProvinceSelection.SelectedItem;
            var casetype = cbxCaseTypeSelection.SelectedIndex;

            if(province == null)
                SeriesCollection.Add(ChartController.GetSeries(country.ToString(), casetype));
            else
                SeriesCollection.Add(ChartController.GetSeries(country.ToString(), province.ToString(), casetype));
        }
    }
}
