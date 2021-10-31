using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;

namespace NOAAWeather
{
    public class NOAAWeatherService
    {
        static string BaseURL = @"https://api.weather.gov";

        private HttpClient http = new HttpClient();

        public NOAAWeatherService()
        {
            //NOAA requires a User-Agent to be set
            http.DefaultRequestHeaders.Add("User-Agent", "nic_side_projects");
        }

        public async Task<Forecast> GetForecastForGridXYAsync(string GridID, string XPoint, string YPoint)
        {
            string url = $"{BaseURL}/gridpoints/{GridID}/{XPoint},{YPoint}/Forecast";

            return await http.GetFromJsonAsync<Forecast>(url);
        }

        public async Task<Observation> GetObservationForStationAsync(string stationId)
        {
            // This is the correct way to get the latest observation but sometimes
            // all of the values are null. The one below that gets all the recent observations
            // has the correct information every time. So, I am using it for now.
            //String url = $"https://api.weather.gov/stations/{stationId}/observations/latest";
            //return await http.GetFromJsonAsync<Observation>(url);



            //Get All Observations and return the first one to work around the above issue
            String url = $"https://api.weather.gov/stations/{stationId}/observations";
            var all =  await http.GetFromJsonAsync<AllObservations>(url);

            if(all != null && all.Feature.Count > 0)
                return all.Feature[0];
            return null;
        }
    }
}

