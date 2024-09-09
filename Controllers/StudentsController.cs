using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Entities;
using StudentManagementSystem.Repositories;
using StudentManagementSystem.ViewModels.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem.Controllers
{
    public class StudentsController : Controller
    {
        public IActionResult Index()
        {
            if (String.IsNullOrEmpty(this.HttpContext.Session.GetString("loggedUser")))
            {
                return RedirectToAction("Login", "Home", new { Url = "/Students/Index"});
            }

            StudentSystemDbContext context = new StudentSystemDbContext();

            IndexVM model = new IndexVM();
            model.Items = context.Students.ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            StudentSystemDbContext context = new StudentSystemDbContext();

            Student item = new Student();

            item.FirstName = model.FirstName;
            item.LastName = model.LastName;
            item.Specialty = model.Specialty;
            item.Year = model.Year;
            item.Username = model.Username;
            item.Password = model.Password;

            context.Students.Add(item);
            context.SaveChanges();

            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (String.IsNullOrEmpty(this.HttpContext.Session.GetString("loggedUser")))
                return RedirectToAction("Login", "Home");

            StudentSystemDbContext context = new StudentSystemDbContext();
            Student item = context.Students.Where(u => u.Id == id)
                                     .FirstOrDefault();

            EditVM model = new EditVM();
            model.Id = item.Id;
            model.FirstName = item.FirstName;
            model.LastName = item.LastName;
            model.Specialty = item.Specialty;
            model.Year = item.Year;
            model.Username = item.Username;
            model.Password = item.Password;
            

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
            Student item = new Student();

            item.Id = model.Id;
            item.FirstName = model.FirstName;
            item.LastName = model.LastName;
            item.Specialty = model.Specialty;
            item.Year = model.Year;
            item.Username = model.Username;
            item.Password = model.Password;

            context.Students.Update(item);
            context.SaveChanges();

            return RedirectToAction("Index", "Students");
        }
        public IActionResult Delete(int id)
        {
            StudentSystemDbContext context = new StudentSystemDbContext();

            Student item = new Student();
            item.Id = id;

            context.Students.Remove(item);
            context.SaveChanges();

            return RedirectToAction("Index", "Students");
        }

        public IActionResult Details (DetailsVM model)
        {
            if (String.IsNullOrEmpty(this.HttpContext.Session.GetString("loggedUser")))
                return RedirectToAction("Login", "Home");

            StudentSystemDbContext context = new StudentSystemDbContext();

            model.Student = context.Students
                                   .Where(s => s.Id == model.StudentId)
                                   .FirstOrDefault();

            model.Items = context.Enrollments
                                 .Where(e => e.StudentId == model.StudentId)
                                 .Include(e => e.Course)
                                 .ToList();

            return View(model);
        }

        public IActionResult DetailsDelete(int StudentId, int CourseId)
        {
            StudentSystemDbContext context = new StudentSystemDbContext();

            Enrollment item = new Enrollment();
            item.StudentId = StudentId;
            item.CourseId = CourseId;

            context.Enrollments.Remove(item);
            context.SaveChanges();


            return Redirect($"/Students/Details?StudentId={StudentId}");

        }
    }
}
