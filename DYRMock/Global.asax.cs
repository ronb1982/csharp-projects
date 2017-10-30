using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using DYRMock.App_Start;

namespace DYRMock
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
