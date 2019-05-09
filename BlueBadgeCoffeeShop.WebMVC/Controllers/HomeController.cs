using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueBadgeCoffeeShop.WebMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (!User.IsInRole("Admin") && !User.IsInRole("Customer"))
            {
                return RedirectToAction("Create", "Customer");
            }
            else if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Product");
            }
            else
                return RedirectToAction("Index", "Admin");

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}