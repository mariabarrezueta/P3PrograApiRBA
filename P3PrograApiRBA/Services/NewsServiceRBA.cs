using Newtonsoft.Json;
using P3PrograApiRBA.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace P3PrograApiRBA.Services
{
    public class NewsServiceRBA
    {
        private readonly string apiKey = "pub_48315332878681a2ca22cde3496dfd2922eec";
        private readonly string baseUrl = "https://newsdata.io/api/1/news?apikey=pub_48315332878681a2ca22cde3496dfd2922eec&q=ecuador";

        public async Task<List<NewsRBA>> GetNews()
        {
            List<NewsRBA> newsList = new List<NewsRBA>();
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(baseUrl)
            };

            try
            {
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    var newsApiResponse = JsonConvert.DeserializeObject<NewsApiResponse>(body);
                    newsList = newsApiResponse.Results;
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            return newsList;
        }
    }
}



