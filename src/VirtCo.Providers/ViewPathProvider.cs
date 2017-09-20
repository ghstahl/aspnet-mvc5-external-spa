using System;
using System.Web.Caching;
using System.Web.Hosting;
using VirtCo.Providers.Stores;

namespace VirtCo.Providers
{
    public class ViewPathProvider : VirtualPathProvider
    {
        private IRazorLocationStore _razorLocationStore;
        private Pages Pages { get; }
        public ViewPathProvider(IRazorLocationStore razorLocationStore)
        {
            this._razorLocationStore = razorLocationStore;
            Pages = new Pages(_razorLocationStore);
        }

        public override bool FileExists(string virtualPath)
        {
            bool bExists = false;
            if (base.FileExists(virtualPath))
            {
                // on disk always wins, remote is second, so don't have conflicts
                return true;
            }

            bExists = Pages.IsExistByVirtualPath(virtualPath);
            return bExists;
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            if (base.FileExists(virtualPath))
            {
                // on disk always wins, remote is second, so don't have conflicts
                return base.GetFile(virtualPath);
            }

            if (Pages.IsExistByVirtualPath(virtualPath))
            {
                return new ViewFile(Pages, virtualPath);
            }

            return null;
        }

        public override CacheDependency GetCacheDependency(string virtualPath, System.Collections.IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            if (Pages.IsExistByVirtualPath(virtualPath))
                return ViewCacheDependencyManager.Instance.Get(virtualPath);

            return Previous.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
        }
    }
}