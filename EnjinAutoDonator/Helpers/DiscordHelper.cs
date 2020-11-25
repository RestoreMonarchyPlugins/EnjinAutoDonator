using Newtonsoft.Json.Linq;
using Rocket.Core.Logging;
using EnjinAutoDonator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EnjinAutoDonator.Helpers
{
    public class DiscordHelper
    {
        private static EnjinAutoDonatorPlugin pluginInstance => EnjinAutoDonatorPlugin.Instance;

        public static void SendNotification(FinishedPurchase purchase)
        {
            var obj = new JObject();
            obj["embeds"] = new JArray
            {
                JToken.FromObject(new
                {
                    color = Convert.ToInt32(pluginInstance.Configuration.Instance.DiscordWebhookColor.Trim('#'), 16),
                    description = pluginInstance.Translate("DiscordNotification", purchase.SteamName, purchase.SteamId, purchase.ItemName)
                })
            };

            using (var wc = new WebClient())
            {
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                wc.UploadString(pluginInstance.Configuration.Instance.DiscordWebhookUrl, obj.ToString());
            }
        }
    }
}
