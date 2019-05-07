using BBCoffeeShop.Models.Admin;
using BBCoffeeShop.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueBadgeCoffeeShop.WebMVC.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            var service = CreateAdminService();
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
        public ActionResult Create(AdminCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateAdminService();

            if (service.CreateAdmin(model))
            {
                TempData["SaveResult"] = "Admin was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Admin could not be created.");

            return View(model);
        }
        public ActionResult Details(int id)
        {
            var svc = CreateAdminService();
            var model = svc.GetAdminByID(id);

            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var service = CreateAdminService();
            var detail = service.GetAdminByID(id);
            var model =
                new AdminUpdate
                {
                    AdminID = detail.AdminID,
                    AdminName = detail.AdminName
                };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AdminUpdate model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.AdminID != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            var service = CreateAdminService();
            if (service.UpdateAdmin(model))
            {
                TempData["SaveResult"] = "Your customer was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Your customer could not be updated.");
            return View(model);
        }
        [Authorize(Roles = "Manager")]
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateAdminService();
            var model = svc.GetAdminByID(id);

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateAdminService();
            service.DeleteAdmin(id);
            TempData["SaveResult"] = "Your note was deleted";
            return RedirectToAction("Index");
        }
        private AdminService CreateAdminService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new AdminService(userId);
            return service;
        }
    }
}