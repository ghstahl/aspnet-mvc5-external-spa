using System;
using System.Web.Caching;

namespace VirtCo.Providers
{
    public class ViewCacheDependency : CacheDependency
    {
        public ViewCacheDependency(string virtualPath)
        {
            base.SetUtcLastModified(DateTime.UtcNow);
        }

        public void Invalidate()
        {
            base.NotifyDependencyChanged(this, EventArgs.Empty);
        }
    }
}