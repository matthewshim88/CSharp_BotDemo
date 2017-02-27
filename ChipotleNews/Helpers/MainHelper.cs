using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using Microsoft.Bot.Connector;

namespace ChipotleNews.Helpers
{
    public class MainHelper
    {
        public async static Task<List<NewsInfo>> GetTrendingNewsAsync()
        {
            List<NewsInfo> newsResults = new List<NewsInfo>();
            const string bingAPIKey = "123165f3200a4b0c9bb36f4d3087410d";
            string queryUri = "https://api.cognitive.microsoft.com/bing/v5.0/news/search";

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", bingAPIKey);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            string bingRawResponse; //raw response from REST endpoint
            TrendingNewsInfo bingJsonResponse = null; //Deserialized response 

            try
            {
                bingRawResponse = await httpClient.GetStringAsync(queryUri);
                bingJsonResponse = JsonConvert.DeserializeObject<TrendingNewsInfo>(bingRawResponse);
            }
            catch (Exception ex)
            {
            }
             newsResults = bingJsonResponse.value.ToList();

            return newsResults;
        }

    }
}