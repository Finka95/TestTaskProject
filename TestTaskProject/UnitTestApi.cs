using System;
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TestTaskProject
{
    public class UnitTestApi
    {
        private HttpClient client;
        private const string BASE_URL = "https://www.metaweather.com";

        [OneTimeSetUp]
        public void Setup()
        {
            client = new HttpClient();
        }

        // Сheck match coordinates
        // Сoordinates for Minsk are taken from dateandtime.info
        [TestCase("/api/location/search/?query=min", "53.90000,27.566670")]
        public async Task LicationSearch(string requestUrl, string realLocationMinsk)
        {
            // Arrange
            string locationMinskFromApi;

            // Act
            HttpResponseMessage response = await client.GetAsync(BASE_URL + requestUrl);
            response.EnsureSuccessStatusCode();
            List<LocationSearch> ls = await response.Content.ReadAsAsync<List<LocationSearch>>();
            locationMinskFromApi = ls[ls.FindIndex(n => n.title == "Minsk")].latt_long;

            // Assert
            Assert.AreEqual(locationMinskFromApi, realLocationMinsk);
        }

        // Сheck temperature interval
        // Some temperature diopason is the average air temperature in Minsk in March (-4°C - +4°C)
        [TestCase("/api/location/search/?query=minsk", "/api/location/", new float[] {-4, 4})]
        public async Task Location(string requestUrlForWoeid, string requestUrlLocation, float[] averageTempInMart)
        {
            // Arrange
            HttpResponseMessage response;
            List<LocationSearch> ls;
            Location location;

            // Act
            // Find woeid
            response = await client.GetAsync(BASE_URL + requestUrlForWoeid);
            response.EnsureSuccessStatusCode();
            ls = await response.Content.ReadAsAsync<List<LocationSearch>>();
            // Find weather
            response = await client.GetAsync(BASE_URL + requestUrlLocation + ls[0].woeid);
            response.EnsureSuccessStatusCode();
            location = await response.Content.ReadAsAsync<Location>();

            // Assert
            foreach (var day in location.consolidated_weather)
            {
                Assert.IsFalse(day.the_temp < averageTempInMart[0] || day.the_temp > averageTempInMart[1]);
            }
        }

        // Find at least one day off the list
        [TestCase("/api/location/search/?query=minsk", "/api/location/")]
        public async Task LocationDay(string requestUrlForWoeid, string requestUrlLocationDay)
        {
            // Arrange
            string requestUrlWoeidAndDate;
            string weatherStateNameToday;
            HttpResponseMessage response;
            List<LocationSearch> ls;
            List<LocationDay> locationDays;
            Location location;

            // Act
            // Find woeid
            response = await client.GetAsync(BASE_URL + requestUrlForWoeid);
            response.EnsureSuccessStatusCode();
            ls = await response.Content.ReadAsAsync<List<LocationSearch>>();
            // Find today's weather
            response = await client.GetAsync(BASE_URL + requestUrlLocationDay + ls[0].woeid);
            response.EnsureSuccessStatusCode();
            location = await response.Content.ReadAsAsync<Location>();
            weatherStateNameToday = location.consolidated_weather[0].weather_state_name;
            // Find weather 5 years ago
            requestUrlWoeidAndDate = string.Format("{0}{1}{2}/{3}/{4}/{5}", BASE_URL, requestUrlLocationDay, ls[0].woeid, DateTime.Now.Year - 5, DateTime.Now.Month, DateTime.Now.Day);
            response = await client.GetAsync(requestUrlWoeidAndDate);
            response.EnsureSuccessStatusCode();
            locationDays = await response.Content.ReadAsAsync<List<LocationDay>>();

            // Assert
            Assert.AreNotEqual(-1, locationDays.FindIndex(n => n.weather_state_name == weatherStateNameToday));
        }
    }
}
