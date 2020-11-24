using Newtonsoft.Json;

namespace EnjinAutoDonator.Models.Enjin
{
    public class EnjinRequestBody
    {
        public string jsonrpc { get; set; }
        public string id { get; set; }
        [JsonProperty(PropertyName = "params")]
        public Params parameters { get; set; }
        public string method { get; set; }
        
        public class Params
        {
            public string api_key { get; set; }
            public string preset_id { get; set; }
            public string date_start { get; set; }
        }
    }
}
