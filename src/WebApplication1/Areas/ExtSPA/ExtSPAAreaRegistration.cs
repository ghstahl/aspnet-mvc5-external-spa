using System.Web.Mvc;

namespace WebApplication1.Areas.ExtSPA
{
    public class ExtSPAAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ExtSPA";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ExtSPA_default",
                "ExtSPA/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "WebApplication1.Areas.ExtSPA.Controllers" }

            );
        }
    }
}