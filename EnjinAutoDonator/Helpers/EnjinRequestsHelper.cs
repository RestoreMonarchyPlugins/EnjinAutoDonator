using Newtonsoft.Json.Linq;
using Rocket.Core.Logging;
using EnjinAutoDonator.Models.Enjin;
using EnjinAutoDonator.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EnjinAutoDonator.Helpers
{
    public class EnjinRequestsHelper
    {
        private static EnjinAutoDonatorPlugin pluginInstance => EnjinAutoDonatorPlugin.Instance;

        private static Random Random => new Random();

        public static JObject GetPurchasesResult(EnjinHttpClient httpClient, int epoch)
        {
            var body = new EnjinRequestBody()
            {
                jsonrpc = "2.0",
                id = Random.Next().ToString(),
                parameters = new EnjinRequestBody.Params()
                {
                    api_key = pluginInstance.Configuration.Instance.APIKey,
                    preset_id = pluginInstance.Configuration.Instance.PresetId.ToString(),
                    date_start = epoch.ToString()
                },
                method = "Shop.getPurchases"
            };

            var response = httpClient.SendRequest(body);

            if (response["id"].ToString() != body.id)
            {
                Logger.LogWarning($"Response ID didn't match the request ID");
                return null;
            }

            return response;
        }
    }
}
