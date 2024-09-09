using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Entities;
using StudentManagementSystem.Repositories;
using StudentManagementSystem.ViewModels.Instructors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem.Controllers
{
    public class InstructorsController : Controller
    {
        public IActionResult Index()
        {
            if (String.IsNullOrEmpty(this.HttpContext.Session.GetString("loggedUser")))
            {
                return RedirectToAction("Login", "Home", new { Url = "/Instructors/Index" });
            }

            StudentSystemDbContext context = new StudentSystemDbContext();

            IndexVM model = new IndexVM();
            model.Items = context.Instructors.ToList();

            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            if (String.IsNullOrEmpty(this.HttpContext.Session.GetString("loggedUser")))
            {
                return RedirectToAction("Login", "Home", new { Url = "/Instructors/Create" });
            }

            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateVM model)
        {
            if (String.IsNullOrEmpty(this.HttpContext.Session.GetString("loggedUser")))
            {
                return RedirectToAction("Login", "Home");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            StudentSystemDbContext context = new StudentSystemDbContext();

            Instructor item = new Instructor();

            item.FirstName = model.FirstName;
            item.LastName = model.LastName;
            item.Rank = model.Rank;
            item.WorkExperience = model.WorkExperience;

            context.Instructors.Add(item);
            context.SaveChanges();

            return RedirectToAction("Index", "Instructors");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (String.IsNullOrEmpty(this.HttpContext.Session.GetString("loggedUser")))
                return RedirectToAction("Login", "Home");

            StudentSystemDbContext context = new StudentSystemDbContext();
            Instructor item = context.Instructors.Where(u => u.Id == id)
                                     .FirstOrDefault();

            EditVM model = new EditVM();
            model.Id = item.Id;
            model.FirstName = item.FirstName;
            model.LastName = item.LastName;
            model.Rank = item.Rank;
            model.WorkExperience = item.WorkExperience;


            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditVM model)
        {
            if (String.IsNullOrEmpty(this.HttpContext.Session.GetString("loggedUser")))
                return RedirectToAction("Login", "Home");

            if (!ModelState.IsValid)
                return View(model);

            StudentSystemDbContext context = new StudentSystemDbContext();
            Instructor item = new Instructor();

            item.Id = model.Id;
            item.FirstName = model.FirstName;
            item.LastName = model.LastName;
            item.Rank = model.Rank;
            item.WorkExperience = model.WorkExperience;

            context.Instructors.Update(item);
            context.SaveChanges();

            return RedirectToAction("Index", "Instructors");
        }
        public IActionResult Delete(int id)
        {
            StudentSystemDbContext context = new StudentSystemDbContext();

            Instructor item = new Instructor();
            item.Id = id;

            context.Instructors.Remove(item);
            context.SaveChanges();

            return RedirectToAction("Index", "Instructors");
        }

        public IActionResult Details(DetailsVM model)
        {
            if (String.IsNullOrEmpty(this.HttpContext.Session.GetString("loggedUser")))
                return RedirectToAction("Login", "Home");

            StudentSystemDbContext context = new StudentSystemDbContext();

            model.Instructor = context.Instructors
                                   .Where(i => i.Id == model.InstructorId)
                                   .FirstOrDefault();

            model.Items = context.Courses
                                 .Where(c => c.InstructorId == model.InstructorId)
                                 .Include(c => c.ParentInstructor)
                                 .ToList();

            return View(model);
        }

        public IActionResult DetailsDelete(int CourseId, int InstructorId)
        {
            StudentSystemDbContext context = new StudentSystemDbContext();

            Course item = new Course();
            item.Id = CourseId;

            context.Courses.Remove(item);
            context.SaveChanges();


            return Redirect($"/Instructors/Details?InstructorId={InstructorId}");

        }
    }
}
