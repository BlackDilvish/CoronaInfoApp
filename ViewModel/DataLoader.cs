using System.Threading.Tasks;
using CoronaInfoAppCore.Model;

namespace CoronaInfoAppCore.ViewModel
{
    public static class DataLoader
    {
        public async static Task<int> UpdateWholeDatabase()
        {
            using(var context = new DataContext())
            {
                var Base = "https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_";
                var countries = DataParser.ParseRequest(RestClient.MakeRequest(Base + "confirmed_global.csv"), 
                                                        RestClient.MakeRequest(Base + "recovered_global.csv"),
                                                        RestClient.MakeRequest(Base + "deaths_global.csv"));

                context.Countries.RemoveRange(context.Countries);
                context.Cases.RemoveRange(context.Cases);

                foreach (var country in countries)
                {
                    await context.AddAsync(country);

                    await context.AddRangeAsync(country.Confirmed);
                    //await context.AddRangeAsync(country.Recovered);
                    await context.AddRangeAsync(country.Deaths);
                }

                return await context.SaveChangesAsync();
            }
        }
    }
}
