using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Entities;
using StudentManagementSystem.Repositories;
using StudentManagementSystem.ViewModels.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult Index()
        {
            if (String.IsNullOrEmpty(this.HttpContext.Session.GetString("loggedUser")))
            {
                return RedirectToAction("Login", "Home", new { Url = "/Courses/Index" });
            }

            StudentSystemDbContext context = new StudentSystemDbContext();

            IndexVM model = new IndexVM();
            model.Items = context.Courses.ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (String.IsNullOrEmpty(this.HttpContext.Session.GetString("loggedUser")))
            {
                return RedirectToAction("Login", "Home", new { Url = "/Courses/Create" });
            }

            StudentSystemDbContext context = new StudentSystemDbContext();

            CreateVM model = new CreateVM();
            model.Instructors = context.Instructors.ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateVM model)
        {
            if (String.IsNullOrEmpty(this.HttpContext.Session.GetString("loggedUser")))
            {
                return RedirectToAction("Login", "Home");
            }

            StudentSystemDbContext context = new StudentSystemDbContext();

            if (!ModelState.IsValid)
            {
                model.Instructors = context.Instructors.ToList();
                return View(model);
            }

            Course item = new Course();

            item.CourseName = model.CourseName;
            item.InstructorId = model.InstructorId;
            item.Duration = model.Duration;
            item.CourseDescription = model.CourseDescription;

            context.Courses.Add(item);
            context.SaveChanges();

            return RedirectToAction("Index", "Courses");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (String.IsNullOrEmpty(this.HttpContext.Session.GetString("loggedUser")))
                return RedirectToAction("Login", "Home");

            StudentSystemDbContext context = new StudentSystemDbContext();
            Course item = context.Courses.Where(c => c.Id == id)
                                     .FirstOrDefault();

            EditVM model = new EditVM();
            model.Id = item.Id;
            model.InstructorId = item.InstructorId;
            model.CourseName = item.CourseName;
            model.Duration = item.Duration;
            model.CourseDescription = item.CourseDescription;
            model.Instructors = context.Instructors.ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditVM model)
        {
            if (String.IsNullOrEmpty(this.HttpContext.Session.GetString("loggedUser")))
                return RedirectToAction("Login", "Home");

            StudentSystemDbContext context = new StudentSystemDbContext();

            if (!ModelState.IsValid)
            {
                model.Instructors = context.Instructors.ToList();
                return View(model);
            }
            
            Course item = new Course();

            item.Id = model.Id;
            item.InstructorId = model.InstructorId;
            item.CourseName = model.CourseName;
            item.Duration = model.Duration;
            item.CourseDescription = model.CourseDescription;

            context.Courses.Update(item);
            context.SaveChanges();

            return RedirectToAction("Index", "Courses");
        }

        public IActionResult Delete(int id)
        {
            StudentSystemDbContext context = new StudentSystemDbContext();

            Course item = new Course();
            item.Id = id;

            context.Courses.Remove(item);
            context.SaveChanges();

            return RedirectToAction("Index", "Courses");
        }

        public IActionResult Details(DetailsVM model)
        {
            if (String.IsNullOrEmpty(this.HttpContext.Session.GetString("loggedUser")))
                return RedirectToAction("Login", "Home");

            StudentSystemDbContext context = new StudentSystemDbContext();

            //we already have model.CourseId from the request
            //and we use it to fill our model with the data we need

            model.Course = context.Courses
                                  .Where(c => c.Id == model.CourseId)
                                  .Include(c => c.ParentInstructor)
                                  .FirstOrDefault();

            model.Items = context.Enrollments
                                 .Where(e => e.CourseId == model.CourseId)
                                 .Include(e => e.Student)
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

            return Redirect($"/Courses/Details?CourseId={CourseId}");

        }
        public IActionResult Enroll(int CourseId)
        {
            StudentSystemDbContext context = new StudentSystemDbContext();

            Enrollment item = new Enrollment();
            item.CourseId = CourseId;
            item.StudentId = Convert.ToInt32(this.HttpContext.Session.GetString("loggedUserId"));

            Enrollment check = context.Enrollments
                                      .Where(e => e.StudentId == item.StudentId && e.CourseId == item.CourseId)
                                      .FirstOrDefault();

            if (check == null)
            {
                context.Enrollments.Add(item);
                context.SaveChanges();
            }

            return Redirect($"/Courses/Details?CourseId={CourseId}");
        }
    }
}
