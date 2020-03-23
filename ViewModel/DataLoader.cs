using System.Threading.Tasks;
using CoronaInfoAppCore.Model;

namespace CoronaInfoAppCore.ViewModel
{
    public static class DataLoader
    {
        public async static Task<int> UpdateDatabase()
        {
            using(var context = new DataContext())
            {
                var Base = "https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_19-covid-";
                var countries = DataParser.ParseRequest(RestClient.MakeRequest(Base + "Confirmed.csv"), 
                                                        RestClient.MakeRequest(Base + "Recovered.csv"),
                                                        RestClient.MakeRequest(Base + "Deaths.csv"));

                context.Countries.RemoveRange(context.Countries);
                context.Cases.RemoveRange(context.Cases);

                foreach (var country in countries)
                {
                    await context.AddAsync(country);

                    await context.AddRangeAsync(country.Confirmed);
                    await context.AddRangeAsync(country.Recovered);
                    await context.AddRangeAsync(country.Deaths);
                }

                return await context.SaveChangesAsync();
            }
        }
    }
}
