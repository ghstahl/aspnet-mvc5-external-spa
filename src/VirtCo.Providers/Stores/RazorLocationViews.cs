using Newtonsoft.Json;

namespace VirtCo.Providers.Stores
{
    public class RazorLocationViews
    {
        [JsonProperty("views")]
        public RazorLocation[] Views { get; set; }
    }
}