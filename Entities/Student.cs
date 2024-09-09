using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem.Entities
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public string Specialty { get; set; }
        public int Year { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }
    }

}
