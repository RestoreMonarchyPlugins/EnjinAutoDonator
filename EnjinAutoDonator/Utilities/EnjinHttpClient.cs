using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rocket.Core.Logging;
using EnjinAutoDonator.Models.Enjin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EnjinAutoDonator.Utilities
{
    public class EnjinHttpClient
    {
        private EnjinAutoDonatorPlugin pluginInstance => EnjinAutoDonatorPlugin.Instance;
        public string APIUrl { get; set; }

        public EnjinHttpClient()
        {
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(AcceptAllCertifications);
            APIUrl = pluginInstance.Configuration.Instance.WebsiteUrl.Trim('/') + "/api/v1/api.php";
        }

        private bool AcceptAllCertifications(object a, X509Certificate b, X509Chain c, SslPolicyErrors d) => true;

        public JObject SendRequest(EnjinRequestBody body)
        {
            var content = JsonConvert.SerializeObject(body);
            var data = Encoding.ASCII.GetBytes(content);

            var request = WebRequest.CreateHttp(APIUrl);

            // without user agent enjin returns Forbidden (403)
            request.UserAgent = "CustomEnjinAPI.Agent";
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;

            request.Timeout = pluginInstance.Configuration.Instance.RequestTimeoutMiliseconds;

            try
            {
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            } catch (WebException e)
            {
                Logger.LogException(e, $"An exception occurated while writing enjin request body for method {body.method}");
                return null;
            }

            string responseContent;

            try
            {
                using (var response = request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        var reader = new StreamReader(stream);
                        responseContent = reader.ReadToEnd();
                    }
                }
            } catch (WebException e)
            {
                Logger.LogException(e, $"An exception occurated while reading enjin response body for method {body.method}");
                return null;
            }

            return JObject.Parse(responseContent);
        }
    }
}
