using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VirtCo.Providers.Stores;

namespace WebApplication1.Areas.ExtSPA.Controllers
{
    public class HomeController : Controller
    {
        private IExternalSpaStore ExternalSpaStore { get; set; }
        public HomeController(IExternalSpaStore externalSpaStore)
        {
            ExternalSpaStore = externalSpaStore;
        }
        // GET: ExtSPA/Home
        public ActionResult Index(string id)
        {
            var spa = ExternalSpaStore.GetRecord(id);
            // var model = new HtmlString(spa.RenderTemplate);

            return View(spa.View);
        }
    }
}