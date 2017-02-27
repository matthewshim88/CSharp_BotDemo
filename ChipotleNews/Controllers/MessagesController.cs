using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using ChipotleNews.Config;
using Microsoft.Bot.Builder.Dialogs;
using ChipotleNews.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using ChipotleNews.Forms;

namespace ChipotleNews
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            Config.ConnectConfig.Connector = new ConnectorClient(new Uri(activity.ServiceUrl));
            Config.ConnectConfig.CurrentActivity = activity;

            Activity reply = null;

            if (activity.Type == ActivityTypes.Message)
            {
                var userInput = await Helpers.LuisHelper.ParseUserInput(Config.ConnectConfig.CurrentActivity.Text);

                UserIntents userIntent = UserIntents.None;

                Enum.TryParse(userInput.intents.First().intent, true, out userIntent);

                string message = "You Selected " + userIntent.ToString();

                switch (userIntent)
                {
                    case UserIntents.LatestNews:
                        reply = await Helpers.TrendingNewsHelper.GetNews(Config.ConnectConfig.Connector, Config.ConnectConfig.CurrentActivity);
                        break;
                    case UserIntents.Greeting:
                        //TODO: Add a Dialog 
                        break;
                }
                await Conversation.SendAsync(Config.ConnectConfig.CurrentActivity, OrderChipotleDialog);
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private static IDialog<ChipotleNews.Forms.ChipotleOrder> OrderChipotleDialog()
        {
            return Chain.From(() => Microsoft.Bot.Builder.FormFlow.FormDialog.FromForm(ChipotleNews.Forms.ChipotleOrder.BuildForm));
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}