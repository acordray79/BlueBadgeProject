using BBCoffeeShop.Models.Customer;
using BBCoffeeShop.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueBadgeCoffeeShop.WebMVC.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var service = CreateCustomerService();
            var model = service.GetCustomer();
            return View(model);
        }
        // GET: Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateCustomerService();

            if (service.CreateCustomer(model))
            {
                TempData["SaveResult"] = "Your note was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Note could not be created.");

            return View(model);
        }
        public ActionResult Details(int id)
        {
            var svc = CreateCustomerService();
            var model = svc.GetCustomerByID(id);

            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var service = CreateCustomerService();
            var detail = service.GetCustomerByID(id);
            var model =
                new CustomerUpdate
                {
                    CustomerID = detail.CustomerID,
                    CustomerName = detail.CustomerName,
                    CustomerEmail = detail.CustomerEmail,
                    CreditCard = detail.CreditCard
                };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CustomerUpdate model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.CustomerID != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            var service = CreateCustomerService();
            if (service.UpdateCustomer(model))
            {
                TempData["SaveResult"] = "Your customer was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Your customer could not be updated.");
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateCustomerService();
            var model = svc.GetCustomerByID(id);

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateCustomerService();
            service.DeleteCustomer(id);
            TempData["SaveResult"] = "Your note was deleted";
            return RedirectToAction("Index");
        }
        private CustomerService CreateCustomerService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CustomerService(userId);
            return service;
        }
    }
}