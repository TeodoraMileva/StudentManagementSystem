using StudentManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem.ViewModels.Courses
{
    public class DetailsVM
    {

        public int CourseId { get; set; } // save the CourseId that is coming from the request

        public Course Course { get; set; } // get the Course record with this CourseId

        public List<Enrollment> Items { get; set; } // get the Enrollment records for this Course

        
    }
}
