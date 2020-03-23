using System;
using System.Collections.Generic;
using System.Windows;

namespace CoronaInfoAppCore.Model
{
    public static class DataParser
    {
        public static List<Country> ParseRequest(List<string> linesConfirmed, List<string> linesRecovered, List<string> linesDeaths)
        {
            List<Country> countries = new List<Country>();
            linesConfirmed.RemoveAt(0);
            linesRecovered.RemoveAt(0);
            linesDeaths.RemoveAt(0);

            for (int i=0; i < linesConfirmed.Count; i++)
            {
                if (linesConfirmed[i].StartsWith("\"") || linesConfirmed[i].StartsWith(",\""))
                    continue;

                var infoConfirmed = linesConfirmed[i].Split(',');
                var infoRecovered = linesRecovered[i].Split(',');
                var infoDeaths = linesDeaths[i].Split(',');

                try
                {
                    var country = new Country();

                    ParseInfo(country, infoConfirmed);
                    ParseConfirmed(country, infoConfirmed);
                    ParseRecovered(country, infoRecovered);
                    ParseDeaths(country, infoDeaths);

                    countries.Add(country);
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                }

            }

            return countries;
        }

        static void ParseInfo(Country country, string[] info)
        {
            country.Name = info[1];
            country.Province = info[0];
            country.Latitude = info[2];
            country.Longitude = info[3];
        }

        static void ParseConfirmed(Country country, string[] info)
        {
            country.Confirmed = new List<Case>();
            DateTime startDate = new DateTime(2020, 1, 22);

            for (int i = 0; i < info.Length - 4; i++)
                country.Confirmed.Add(new Case()
                {
                    Date = startDate.AddDays(i),
                    CountryName = country.Name,
                    NumberOfCases = int.Parse(info[4 + i]),
                    Type = (int)CaseType.Confirmed
                });
        }

        static void ParseRecovered(Country country, string[] info)
        {
            country.Recovered = new List<Case>();
            DateTime startDate = new DateTime(2020, 1, 22);

            for (int i = 0; i < info.Length - 4; i++)
                country.Recovered.Add(new Case()
                {
                    Date = startDate.AddDays(i),
                    CountryName = country.Name,
                    NumberOfCases = int.Parse(info[4 + i]),
                    Type = (int) CaseType.Recovered
                });
        }

        static void ParseDeaths(Country country, string[] info)
        {
            country.Deaths = new List<Case>();
            DateTime startDate = new DateTime(2020, 1, 22);

            for (int i = 0; i < info.Length - 4; i++)
                country.Deaths.Add(new Case()
                {
                    Date = startDate.AddDays(i),
                    CountryName = country.Name,
                    NumberOfCases = int.Parse(info[4 + i]),
                    Type = (int)CaseType.Deaths
                });
        }
    }
}
