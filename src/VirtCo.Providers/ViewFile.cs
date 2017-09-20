using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.UI;
using VirtCo.Providers.Stores;

namespace VirtCo.Providers
{
    public class ViewFile : VirtualFile
    {
        private string VirtualPath { get; }
        private Pages Pages { get; }
        
        public ViewFile(Pages pages, string virtualPath): base(virtualPath)
        {
            Pages = pages;
            VirtualPath = virtualPath;
        }

        public override Stream Open()
        {
            if (string.IsNullOrEmpty(VirtualPath))
                return new MemoryStream();

            string content = Pages.GetByVirtualPath(VirtualPath);
            if (string.IsNullOrEmpty(content))
                return new MemoryStream();

            return new MemoryStream(ASCIIEncoding.UTF8.GetBytes(content));
        }
    }
}
