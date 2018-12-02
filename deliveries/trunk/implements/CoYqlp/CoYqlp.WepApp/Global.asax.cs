using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CoYqlp.WepApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ///Connfiguration Log4Net
            ///
            log4net.Config.XmlConfigurator.Configure();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        protected void Application_Error(Object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Phien giao dich cua nguoi dung
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Session_Start(object sender, EventArgs e)
        {
            //Session["accountID"] = String.Empty;
            //if (String.IsNullOrEmpty(Session["accountID"].ToString()))
            //    Response.Redirect("~/Admin/Home/Login");
        }

        /// <summary>
        /// Dong phien giao dich
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Session_End(object sender, EventArgs e)
        {
            //Session["accountID"] = String.Empty;
            //Response.Redirect("~/Admin/Home/Login");
        }
    }
}
