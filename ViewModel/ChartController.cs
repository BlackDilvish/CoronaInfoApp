using System;
using System.Collections.Generic;
using System.Text;
using CoronaInfoAppCore.Model;
using LiveCharts;
using LiveCharts.Wpf;
using System.Linq;

namespace CoronaInfoAppCore.ViewModel
{
    class ChartController
    {
        public LineSeries GetSeries(string countryName, int type, DateTime? startDate = null, DateTime? endDate = null)
        {
            var getter = new DataGetter();
            var cases = getter.GetCases(countryName, type, startDate, endDate);

            return new LineSeries()
            {
                Title = $"{((CaseType)type).ToString()} in {countryName}",
                Values = new ChartValues<int>(cases.Select(c => c.NumberOfCases)),
            };
        }

        public LineSeries GetSeries(string countryName, string provinceName, int type, DateTime? startDate = null, DateTime? endDate = null)
        {
            var getter = new DataGetter();
            var cases = getter.GetCases(countryName, provinceName, type, startDate, endDate);

            return new LineSeries()
            {
                Title = $"{((CaseType)type).ToString()} in {countryName} ({provinceName})",
                Values = new ChartValues<int>(cases.Select(c => c.NumberOfCases)),
            };
        }

        public string[] GetLabels(DateTime? startDate = null, DateTime? endDate = null)
        {
            var getter = new DataGetter();
            return getter.GetCases("Poland", 0, startDate, endDate).Select(c => c.Date.ToShortDateString()).ToArray();
        }
    }
}
