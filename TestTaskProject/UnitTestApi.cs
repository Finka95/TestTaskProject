using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TestTaskProject
{
    public class UnitTestApi
    {
        HttpClient client;
        const string BASE_URL = "https://www.metaweather.com";

        [SetUp]
        public void Setup()
        {
            client = new HttpClient();
        }

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
            Assert.Equals(locationMinskFromApi, realLocationMinsk);
        }

        [TestCase("/api/location/search/?query=minsk", "/api/location/")]
        public async Task LocationDay(string requestUrlForWoeid, string requestUrlLocationDay)
        {
            // Arrange
            HttpResponseMessage response;
            List<LocationSearch> ls;
            string requestLocation;
            List<Location> location;

            // Act
            response = await client.GetAsync(BASE_URL + requestUrlForWoeid);
            response.EnsureSuccessStatusCode();
            ls = await response.Content.ReadAsAsync<List<LocationSearch>>();
            requestLocation = string.Format(requestUrlLocationDay, ls[0].woeid);
            response = await client.GetAsync(BASE_URL + requestLocation);
            response.EnsureSuccessStatusCode();
            location = await response.Content.ReadAsAsync<List<Location>>();
        }

        [TearDown]
        public void TearDown()
        {

        }
    }
}

//List<LocationSearch> locationSearch = (List<LocationSearch>)JsonConvert.DeserializeObject(responseContent, typeof(List<LocationSearch>));

