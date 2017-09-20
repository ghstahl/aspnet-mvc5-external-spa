using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace VirtCo.Providers.Stores
{
    public class RemoteRazorLocationStore : InMemoryRazorLocationStore, IRemoteRazorLocationStore
    {
       
        // https://rawgit.com/ghstahl/P7/master/src/p7.external.spa/Areas/ExtSpa/views.json;
        public RemoteRazorLocationStore()
        {

        }
        public static RazorLocationViews FromJson(string json) => JsonConvert.DeserializeObject<RazorLocationViews>(json, Settings);
        public static string ToJson(RazorLocationViews o) => JsonConvert.SerializeObject((object)o, (JsonSerializerSettings)Settings);

        // JsonConverter stuff

        static JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };

        private async Task<List<RazorLocation>> GetRemoteDataAsync(string url)
        {
            try
            {
                var accept = "application/json";
                var uri = url;
                var req = (HttpWebRequest)WebRequest.Create((string)uri);
                req.Accept = accept;
                var content = new MemoryStream();
                RazorLocationViews razorLocationViews;
                using (WebResponse response = await req.GetResponseAsync())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {

                        // Read the bytes in responseStream and copy them to content.
                        await responseStream.CopyToAsync(content);
                        string result = Encoding.UTF8.GetString(content.ToArray());
                        razorLocationViews = FromJson(result);
                    }
                }
                var now = DateTime.UtcNow;

                var query = from item in razorLocationViews.Views
                    let c = new RazorLocation(item) { LastModified = now, LastRequested = now }
                    select c;

                return query.ToList();
            }
            catch (Exception e)
            {
                throw;
            }
             
        }

        public async Task LoadRemoteDataAsync(string url)
        {
            var result = await GetRemoteDataAsync(url);
            Insert(result);
        }
    }
}