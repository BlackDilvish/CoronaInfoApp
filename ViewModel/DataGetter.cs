using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CoronaInfoAppCore.Model;

namespace CoronaInfoAppCore.ViewModel
{
    class DataGetter
    {
        public List<Country> GetCountries()
        {
            using var context = new DataContext();
            return context.Countries.ToList();
        }

        public List<string> GetProvinces(string countryName)
        {
            using var context = new DataContext();
            return context.Countries.Where(c => c.Name.Equals(countryName)).Select(c => c.Province).ToList();
        }

        public List<Case> GetCases(string countryName, int type, DateTime? startDate = null, DateTime? endDate = null)
        {
            return GetCases(countryName, "", type, startDate, endDate);
        }

        public List<Case> GetCases(string countryName, string countryProvince, int type, DateTime? startDate = null, DateTime? endDate = null)
        {
            using var context = new DataContext();
            return context.Cases.Where(c => c.CountryName.Equals(countryName)
                                         && c.CountryProvince.Equals(countryProvince)
                                         && c.Type.Equals(type)
                                         && c.Date >= (startDate ?? new DateTime(2020, 1, 22))
                                         && c.Date <= (endDate ?? DateTime.Today))
                                            .OrderBy(c => c.Date).ToList();
        }
    }
}
