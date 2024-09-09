using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem.Repositories
{
    public class StudentSystemDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Enrollment> Enrollments { get; set; }

        public StudentSystemDbContext()
        {
            this.Students = this.Set<Student>();
            this.Instructors = this.Set<Instructor>();
            this.Courses = this.Set<Course>();
            this.Enrollments = this.Set<Enrollment>();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrollment>().HasKey(e => new { e.StudentId, e.CourseId });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=StudentSystemDb;User Id=teodoramileva;Password=tedipass;");
        }
    }
}
