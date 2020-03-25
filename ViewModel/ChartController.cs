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
        public LineSeries GetSeries(string countryName, CaseType type, DateTime? startDate = null, DateTime? endDate = null)
        {
            var getter = new DataGetter();
            var cases = getter.GetCases(countryName, type, startDate, endDate);

            return new LineSeries()
            {
                Title = $"{type.ToString()} in {countryName}",
                Values = new ChartValues<int>(cases.Select(c => c.NumberOfCases)),
            };
        }

        public string[] GetLabels(DateTime? startDate = null, DateTime? endDate = null)
        {
            var getter = new DataGetter();
            return getter.GetCases("Poland", CaseType.Confirmed, startDate, endDate).Select(c => c.Date.ToShortDateString()).ToArray();
        }
    }
}
