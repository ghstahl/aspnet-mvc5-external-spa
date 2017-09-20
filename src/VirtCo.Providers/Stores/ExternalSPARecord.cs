using Newtonsoft.Json;

namespace VirtCo.Providers.Stores
{
    public class ExternalSPARecord
    {
        [JsonProperty("renderTemplate")]
        public string RenderTemplate { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("requireAuth")]
        public bool RequireAuth { get; set; }

        [JsonProperty("view")]
        public string View { get; set; }
    }
}