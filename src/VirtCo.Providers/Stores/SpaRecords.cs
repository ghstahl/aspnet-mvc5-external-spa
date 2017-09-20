using Newtonsoft.Json;

namespace VirtCo.Providers.Stores
{
    public class SpaRecords
    {
        [JsonProperty("spas")]
        public ExternalSPARecord[] Spas { get; set; }
    }
}