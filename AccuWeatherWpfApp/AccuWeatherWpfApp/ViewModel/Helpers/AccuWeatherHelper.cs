﻿using WeatherApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.ViewModel.Helpers
{
    public class AccuWeatherHelper
    {
        public const string BASE_URL = "http://dataservice.accuweather.com/";

        public const string AUTOCOMPLETE_ENDPOINT = "locations/v1/cities/autocomplete?apikey={0}&q={1}";
        public const string CURRENT_CONDITIONS_ENDPOINT = "currentconditions/v1/{0}?apikey={1}";
        public const string API_KEY = "zAF58ZlVydwP9FTbq72zGtiQHwIQDBBo";

        public static async Task<List<City>> GetCities(string query)
        {
            List<City> lstCities = new List<City>();
            string URL = BASE_URL + string.Format(AUTOCOMPLETE_ENDPOINT, API_KEY, query);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(URL);
                string json = await response.Content.ReadAsStringAsync();
                lstCities = JsonConvert.DeserializeObject<List<City>>(json);
            }

            return lstCities;
        }

        public static async Task<CurrentConditions> GetCurrentConditions(string citykey)
        {
            CurrentConditions currentConditions = new CurrentConditions();

            string URL = BASE_URL + string.Format(CURRENT_CONDITIONS_ENDPOINT, citykey, API_KEY);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(URL);
                string json = await response.Content.ReadAsStringAsync();
                currentConditions = JsonConvert.DeserializeObject<List<CurrentConditions>>(json).FirstOrDefault();
            }

            return currentConditions;
        }
    }
}
