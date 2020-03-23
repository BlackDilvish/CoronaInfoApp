using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CoronaInfoAppCore.Model;

namespace CoronaInfoAppCore.ViewModel
{
    class DataGetter
    {
        public List<Case> GetCases(string countryName, CaseType type, DateTime? startDate = null, DateTime? endDate = null)
        {
            using var context = new DataContext();
            return context.Cases.Where(c => c.CountryName.Equals(countryName) 
                                         && c.Type.Equals((int)type) 
                                         && c.Date >= (startDate ?? new DateTime(2020, 1, 22)) 
                                         && c.Date <= (endDate ?? DateTime.Today))
                                            .OrderBy(c => c.Date).ToList();
        }
    }
}
