using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using VirtCo.Providers.Stores;

namespace VirtCo.Providers
{
    public class Pages
    {
        private readonly IRazorLocationStore _razorLocationStore;

        public Pages(IRazorLocationStore razorLocationStore)
        {
            _razorLocationStore = razorLocationStore;
        }

        public bool IsExistByVirtualPath(string virtualPath)
        {
            return IsExistByVirtualPathAsync(virtualPath).GetAwaiter().GetResult();
        }

        public  async Task<bool> IsExistByVirtualPathAsync(string virtualPath)
        {
            if (virtualPath.StartsWith("~/"))
                virtualPath = virtualPath.Substring(1);

            var query = new RazorLocationQuery() { Location = virtualPath };

            var doc = await _razorLocationStore.FetchAsync(query);
            if (doc != null)
            {
                return true;
            }
            return false;
        }

        public string GetByVirtualPath(string virtualPath)
        {
            return GetByVirtualPathAsync(virtualPath).GetAwaiter().GetResult();
        }

        public async Task<string> GetByVirtualPathAsync(string virtualPath)
        {
            if (virtualPath.StartsWith("~/"))
                virtualPath = virtualPath.Substring(1);

            var query = new RazorLocationQuery() { Location = virtualPath };

            var doc = await _razorLocationStore.FetchAsync(query);
            if (doc != null)
            {
            //    var viewContent = Encoding.UTF8.GetBytes(doc.Content);
            //    var lastModified = doc.LastModified;
                doc.LastRequested = DateTime.UtcNow;
                await _razorLocationStore.UpdateAsync(doc);
                return doc.Content;
            }
            return null;
        }
    }
}