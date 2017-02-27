using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using System.Net.Http;
using Newtonsoft.Json;

namespace ChipotleNews.Helpers
{
    public class TrendingNewsHelper
    {
        public async static Task<Activity> GetNews(ConnectorClient connector, Activity activity)
        {
            var newsResults = await GetTrendingNewsAsync();
            Activity replyMessage = activity.CreateReply("Here's the latest news I found:");
            replyMessage.Recipient = activity.From;
            replyMessage.Type = ActivityTypes.Message;
            replyMessage.AttachmentLayout = "carousel";
            replyMessage.Attachments = new List<Attachment>();

            foreach (var result in newsResults)
            {
                Attachment attachment = new Attachment();
                attachment.ContentType = "application/vnd.microsoft.card.hero";

                HeroCard heroCard = new HeroCard();
                heroCard.Title = result.name;
                heroCard.Subtitle = result.description;
                heroCard.Images = new List<CardImage>();

                CardImage thumbnailImage = new CardImage();
                thumbnailImage.Url = result.img.thumbnail.contentURL;
                heroCard.Images.Add(thumbnailImage);

                heroCard.Buttons = new List<CardAction>();
                CardAction articleCard = new CardAction();

                articleCard.Title = "View article";
                articleCard.Type = "openUrl";

                articleCard.Value = result.URL;
                heroCard.Buttons.Add(articleCard);

                attachment.Content = heroCard;

                replyMessage.Attachments.Add(attachment);
            }
            return replyMessage;
        }

        public async static Task<List<NewsInfo>> GetTrendingNewsAsync()
        {
            List<NewsInfo> newsResults = new List<NewsInfo>();

            const string bingAPIKey = "123165f3200a4b0c9bb36f4d3087410d";
            string queryUri = "https://api.cognitive.microsoft.com/bing/v5.0/news/search";

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", bingAPIKey);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            string bingRawResponse;
            TrendingNewsInfo bingJsonResponse = null;

            try
            {
                bingRawResponse = await httpClient.GetStringAsync(queryUri);
                bingJsonResponse = JsonConvert.DeserializeObject<TrendingNewsInfo>(bingRawResponse);
            }
            catch (Exception ex)
            {
            }
            NewsInfo[] newsResult = bingJsonResponse.value;

            if (newsResult == null || newsResult.Length == 0)
            {
                //add code to handle the case where results are null are zero
            }

            return newsResults;
        }
    }
}