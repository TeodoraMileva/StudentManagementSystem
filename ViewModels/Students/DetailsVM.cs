using StudentManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem.ViewModels.Students
{
    public class DetailsVM
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public List<Enrollment> Items { get; set; }
    }
}
