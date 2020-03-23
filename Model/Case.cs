using System;
using System.Collections.Generic;
using System.Text;

namespace CoronaInfoAppCore.Model
{
    public enum CaseType { Confirmed, Recovered, Deaths };

    public class Case
    {
        public int ID { get; set; }
        public string CountryName { get; set; }
        public DateTime Date { get; set; }
        public int NumberOfCases { get; set; }
        public int Type { get; set; }
    }
}
