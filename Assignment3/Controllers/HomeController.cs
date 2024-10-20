using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment3.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Report()
        {
            ViewBag.Message = "Report";

            return View();
        }

        public ActionResult Maintain()
        {
            ViewBag.Message = "Maintain Page";

            return View();
        }
    }
}