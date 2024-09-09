using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem.Entities
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public int InstructorId { get; set; }
        public string CourseName { get; set; }
        public int Duration { get; set; }
        public string CourseDescription { get; set; }

        [ForeignKey("InstructorId")]
        public Instructor ParentInstructor { get; set; }
    }
}
