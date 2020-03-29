using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Linq;
using CoronaInfoAppCore.Model;

namespace CoronaInfoAppCore.ViewModel
{
    class ComboBoxController
    {
        public void FillCountrySelector(ComboBox comboBox)
        {
            var getter = new DataGetter();

            var countries = getter.GetCountries().Select(c => c.Name).Distinct().ToList();
            countries.Sort();

            foreach (var country in countries)
                comboBox.Items.Add(country);
        }

        public bool FillProvinceSelector(ComboBox comboBox, string countryName)
        {
            var getter = new DataGetter();

            var provinces = getter.GetProvinces(countryName);
            provinces.Remove("");
            provinces.Sort();

            comboBox.Items.Clear();
            if (provinces.Count > 0)
            {
                comboBox.IsEnabled = true;
                foreach (var province in provinces)
                    comboBox.Items.Add(province);

                return true;
            }

            return false;
        }

        public void FillCaseTypeSelector(ComboBox comboBox)
        {
            foreach (var value in Enum.GetValues(typeof(CaseType)))
                comboBox.Items.Add(value);
        }
    }
}
