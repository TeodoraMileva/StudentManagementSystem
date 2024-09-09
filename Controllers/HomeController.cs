using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Entities;
using StudentManagementSystem.Repositories;
using StudentManagementSystem.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(string Url)
        {
            if (!String.IsNullOrEmpty(this.HttpContext.Session.GetString("loggedUser")))
            {
                return RedirectToAction("Index", "Home");
            }

            LoginVM model = new LoginVM();
            model.Url = Url;

            return View(model);
        }

        [HttpPost]
        public IActionResult Login(LoginVM model)
        {
            if (!String.IsNullOrEmpty(this.HttpContext.Session.GetString("loggedUser")))
            {
                return RedirectToAction("Index", "Home");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            StudentSystemDbContext context = new StudentSystemDbContext();

            Student loggedUser = context.Students
                                           .Where(
                                            s => 
                                                s.Username == model.Username &&
                                                s.Password == model.Password
                                           ).FirstOrDefault();

            if (loggedUser == null)
            {
                ModelState.AddModelError("authError", "Incorrect username or password");
                return View(model);
            }

            this.HttpContext.Session.SetString("loggedUser", loggedUser.Username);
            this.HttpContext.Session.SetString("loggedUserId", loggedUser.Id.ToString());
            

            return Redirect(String.IsNullOrEmpty(model.Url) ? "/Home/Index" : model.Url);
        }

        public IActionResult Logout()
        {
            if (String.IsNullOrEmpty(this.HttpContext.Session.GetString("loggedUser")))
            {
                return RedirectToAction("Index", "Home");
            }

            this.HttpContext.Session.Remove("loggedUser");
            this.HttpContext.Session.Remove("loggedUserId");

            return RedirectToAction("Index", "Home");
        }
    }
}
