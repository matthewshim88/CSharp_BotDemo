using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;


namespace ChipotleNews.Helpers
{
    public class LuisHelper
    {
        public static async Task<UserInputInfo> ParseUserInput(string input)
        {
            UserInputInfo userInput = null;

            using (var client = new HttpClient())
            {
                string LuisKey = Config.LuisConfig.LuisKey;
                string LuisAppId = Config.LuisConfig.LuisAppId;
                //string LuisURL = Config.LuisConfig.LuisURL;

                //from the user
                string query = Uri.EscapeDataString(input);

                string uri = $"https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/{LuisAppId}?subscription-key={LuisKey}&q={query}&verbose=true";

                try
                {
                    var response = await client.GetAsync(uri);
                    response.EnsureSuccessStatusCode();

                    var data = await response.Content.ReadAsStringAsync();
                    userInput = JsonConvert.DeserializeObject<UserInputInfo>(data);
                }catch(Exception ex)
                {

                }
                return userInput;
            }

        }
    }
}