using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoYqlp.WepApp.Controllers
{
    public class ErrorController : BaseController
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Error 404
        /// </summary>
        /// <returns></returns>
        public ActionResult NotFound() {
            return View();
        }
    }
}