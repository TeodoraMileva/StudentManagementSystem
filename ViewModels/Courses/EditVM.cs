using StudentManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem.ViewModels.Courses
{
    public class EditVM
    {
        public int Id { get; set; }

        [Display(Name = "Instructor: ")]
        [Required(ErrorMessage = "This field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select an instructor.")]
        public int InstructorId { get; set; }

        [Display(Name = "Course Name: ")]
        [Required(ErrorMessage = "This field is required.")]
        public string CourseName { get; set; }

        [Display(Name = "Duration: ")]
        [Required(ErrorMessage = "This field is required.")]
        [Range(1, 24, ErrorMessage = "Please enter a valid value.")]
        public int Duration { get; set; }

        [Display(Name = "Course Description: ")]
        [Required(ErrorMessage = "This field is required.")]
        public string CourseDescription { get; set; }

        public List<Instructor> Instructors { get; set; }
    }
}
