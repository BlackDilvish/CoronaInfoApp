using CoronaInfoAppCore.ViewModel;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Linq;
using System.Windows.Media;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Animation;

namespace CoronaInfoAppCore.View
{
    public partial class LineChartPage : UserControl, ISwitchable
    {
        private ChartController ChartController { get; set; }
        private ComboBoxController ComboBoxController { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<int, string> YFormatter { get; set; }
        private bool IsMenuEnabled { get; set; } = false;

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

            if (!ComboBoxController.FillProvinceSelector(cbxProvinceSelection, countryName))
            {
                cbxCaseTypeSelection.IsEnabled = true;
                if (cbxCaseTypeSelection.SelectedItem != null)
                {
                    btnCreateChart.IsEnabled = true;
                    btnAddChart.IsEnabled = true;
                }
            }

        }

        private void cbxProvinceSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbxCaseTypeSelection.IsEnabled = true;
        }

        private void cbxDataTypeSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnCreateChart.IsEnabled = true;
            btnAddChart.IsEnabled = true;
        }

        private void btnCreateChart_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SeriesCollection.Clear();
            ChartController.AddSeries(SeriesCollection, 
                                      cbxCountrySelection.SelectedItem.ToString(),
                                     (cbxProvinceSelection.SelectedItem ?? "").ToString(), 
                                      cbxCaseTypeSelection.SelectedIndex);

            btnCreateChart.IsEnabled = false;
            btnAddChart.IsEnabled = false;
        }

        private void btnAddChart_Click(object sender, RoutedEventArgs e)
        {
            ChartController.AddSeries(SeriesCollection,
                                      cbxCountrySelection.SelectedItem.ToString(),
                                     (cbxProvinceSelection.SelectedItem ?? "").ToString(),
                                      cbxCaseTypeSelection.SelectedIndex);

            btnCreateChart.IsEnabled = false;
            btnAddChart.IsEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(IsMenuEnabled)
            {
                var sb = Resources["sbHideLeftMenu"] as Storyboard;
                sb.Begin(pnlMenuPanel);
                IsMenuEnabled = false;
            }
            else
            {
                var sb = Resources["sbShowLeftMenu"] as Storyboard;
                sb.Begin(pnlMenuPanel);
                IsMenuEnabled = true;
            }
        }
        
    }
}
