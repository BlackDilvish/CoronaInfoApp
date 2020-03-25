using CoronaInfoAppCore.ViewModel;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Linq;
using System.Windows.Media;
using System.Collections.Generic;

namespace CoronaInfoAppCore.View
{
    public partial class LineChartPage : UserControl, ISwitchable
    {
        private ChartController ChartController { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<int, string> YFormatter { get; set; }

        public LineChartPage()
        {
            InitializeComponent();



            ChartController = new ChartController();

            SeriesCollection = new SeriesCollection()
            {
                ChartController.GetSeries("Poland", Model.CaseType.Confirmed),
                ChartController.GetSeries("Germany", Model.CaseType.Deaths),
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

        }

        private void cbxProvinceSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbxDataTypeSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnCreateChart_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
