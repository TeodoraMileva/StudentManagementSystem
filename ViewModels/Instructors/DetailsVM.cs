using StudentManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem.ViewModels.Instructors
{
    public class DetailsVM
    {
        public int InstructorId { get; set; } // save the InstructorId that is coming from the request

        public Instructor Instructor { get; set; } // get the Instructor record with this InstructorId

        public List<Course> Items { get; set; } // get the Course records for this Instructor
    }
}
